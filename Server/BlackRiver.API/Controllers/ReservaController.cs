using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackRiver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : Controller
    {
        #region Workers

        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IEnumerable<Reserva>> Get()
        {
            return await DataServices.ReservaService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await DataServices.ReservaService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reserva reserva)
        {
            try
            {
                var result = await DataServices.ReservaService.Create(reserva);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Reserva reserva)
        {
            try
            {
                var result = await DataServices.ReservaService.Update(id, reserva);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await DataServices.ReservaService.Delete(id))
                return Ok();

            return BadRequest("Reserva não existe ou já foi excluída");
        }

        #endregion Workers

        #region Customer

        [HttpGet]
        [Route("token/all")]
        public async Task<IEnumerable<Reserva>> CustomerGetAll()
        {
            var all = await DataServices.ReservaService.GetAll();
            var user = await GetHospede();
            return all.Where(r => r.HospedeId == user.Id);
        }

        [HttpGet]
        [Route("token")]
        public async Task<Reserva> CustomerGet()
        {
            try
            {
                var all = await DataServices.ReservaService.GetAll();
                var user = await GetHospede();
                return all.Where(r => r.HospedeId == user.Id).LastOrDefault();
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CustomerPost(DateTime dataInicial, int qtdDias)
        {
            try
            {
                var hospede = await GetHospede();
                var reservas = await DataServices.ReservaService.GetAll();
                var match = reservas.FirstOrDefault(r => r.DataEntrada.Equals(dataInicial));

                if (match != null && match.HospedeId == hospede.Id)
                    throw new Exception("Reserva não pode ser criada, tente outra data");

                var quartos = await DataServices.QuartoService.GetAll();
                var quarto = quartos.FirstOrDefault(q => q.StatusQuarto == (int)QuartoStatus.Disponivel);

                var reserva = new Reserva
                {
                    DataEntrada = dataInicial,
                    DataSaida = dataInicial.AddDays(qtdDias),
                    Status = ReservaStatus.Aberto.ToString(),
                    HospedeId = hospede.Id,
                    QuartoId = quarto.Id
                };

                quarto.StatusQuarto = (int)QuartoStatus.Ocupado;

                await DataServices.QuartoService.Update(quarto.Id, quarto);

                var result = await DataServices.ReservaService.Create(reserva);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("token/cancel")]
        public async Task<IActionResult> CustomerPut(DateTime dataInicial)
        {
            try
            {
                var hospede = await GetHospede();
                var reservas = await DataServices.ReservaService.GetAll();
                var reserva = reservas.FirstOrDefault(r => r.DataEntrada.Equals(dataInicial));

                reserva.Status = ReservaStatus.Cancelado.ToString();
                reserva.DataCancelamento = DateTime.UtcNow;

                var result = await DataServices.ReservaService.Update(reserva.Id, reserva);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion Customer

        private async Task<Hospede> GetHospede()
        {
            var all = await DataServices.HospedeService.GetAll();

            if (all.Any(h => h.Email.Equals(User.Identity.Name)))
                return all.FirstOrDefault(h => h.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));

            return all.FirstOrDefault(h => h.Nome.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
