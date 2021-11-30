using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Reserva> Get(int id)
        {
            using var context = DataServices.HospedeService.ContextFactory.CreateDbContext();

            return await context
                .Set<Reserva>()
                .Include(h => h.Hospedes)
                .Include(h => h.Quarto)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reserva reserva)
        {
            try
            {
                var reservas = await DataServices.ReservaService.GetAll();
                var match = reservas.FirstOrDefault(r => r.DataEntrada.Equals(reserva.DataEntrada));

                if (match != null && match.Hospedes.Any(h => h.Id == reserva.Hospedes[0].Id))
                    throw new Exception("Reserva não pode ser criada, tente outra data");

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
            return all.Where(r => user.Reservas.Any(u => u.Id == r.Id));
        }

        [HttpGet]
        [Route("token")]
        public async Task<Reserva> CustomerGet()
        {
            try
            {
                var user = await GetHospede();
                return user.Reservas.LastOrDefault();
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
                var reserva = await CreateReserva(dataInicial, qtdDias, hospede);

                if (reserva == null)
                    throw new Exception("Reserva não pode ser criada, tente outra data");

                _ = await DataServices.HospedeService.Update(hospede.Id, hospede);
                _ = await DataServices.ReservaService.Update(reserva.Id, reserva);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("token/cancel")]
        public async Task<IActionResult> CustomerPut(DateTime dataInicial)
        {
            try
            {
                var hospede = await GetHospede();
                var reserva = hospede.Reservas.FirstOrDefault(r => r.DataEntrada.Equals(dataInicial));

                reserva.Quarto = null;
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
            using var context = DataServices.HospedeService.ContextFactory.CreateDbContext();

            var all = await context
                .Set<Hospede>()
                .Include(h => h.Ocorrencias)
                .Include(h => h.VagaEstacionamento)
                .Include(h => h.Reservas)
                .ThenInclude(r => r.Quarto)
                .ToListAsync();

            if (all.Any(h => h.Email.Equals(User.Identity.Name)))
                return all.FirstOrDefault(h => h.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));

            return all.FirstOrDefault(h => h.Nome.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        private async Task<Reserva> CreateReserva(DateTime dataInicial, int qtdDias, Hospede user)
        {
            var reservas = await DataServices.ReservaService.GetAll();
            var match = reservas.FirstOrDefault(r => r.DataEntrada.Equals(dataInicial));

            if (match != null && match.Hospedes.Any(h => h.Id == user.Id))
                return null;

            var quartos = await DataServices.QuartoService.GetAll();
            var quarto = quartos.FirstOrDefault(q => q.StatusQuarto == (int)QuartoStatus.Disponivel);

            return new Reserva
            {
                DataEntrada = dataInicial,
                DataSaida = dataInicial.AddDays(qtdDias),
                Status = ReservaStatus.Aberto.ToString(),
                Hospedes = new() { user },
                Quarto = quarto
            };
        }
    }
}
