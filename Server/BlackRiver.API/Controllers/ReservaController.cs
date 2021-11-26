using BlackRiver.Data;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : Controller
    {
        private readonly GenericDataService<Reserva> reservaService = new(new BlackRiverDBContextFactory());
        private readonly GenericDataService<Hospede> hospedeService = new(new BlackRiverDBContextFactory());
        private readonly GenericDataService<Quarto> quartoService = new(new BlackRiverDBContextFactory());

        #region Workers

        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IEnumerable<Reserva>> Get()
        {
            return await reservaService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<Reserva> Get(int id)
        {
            return await reservaService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reserva reserva)
        {
            try
            {
                var result = await reservaService.Create(reserva);
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
                var result = await reservaService.Update(id, reserva);
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
            if (await reservaService.Delete(id))
                return Ok();

            return BadRequest("Reserva não existe ou já foi excluída");
        }

        #endregion Workers

        #region Customer

        [HttpGet]
        [Route("token/all")]
        public async Task<IEnumerable<Reserva>> CustomerGetAll()
        {
            var all = await reservaService.GetAll();
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

                _ = await hospedeService.Update(hospede.Id, hospede);
                _ = await reservaService.Update(reserva.Id, reserva);

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
                reserva.Status = "Cancelado";
                reserva.DataCancelamento = DateTime.UtcNow;

                var result = await reservaService.Update(reserva.Id, reserva);
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
            var factory = new BlackRiverDBContextFactory();

            using var context = factory.CreateDbContext();

            var all = await context
                .Set<Hospede>()
                .Include(h => h.Reservas)
                .ThenInclude(r => r.Quarto)
                .ToListAsync();

            if (all.Any(h => h.Email.Equals(User.Identity.Name)))
                return all.FirstOrDefault(h => h.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));

            return all.FirstOrDefault(h => h.Nome.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        private async Task<Reserva> CreateReserva(DateTime dataInicial, int qtdDias, Hospede user)
        {
            var quartos = await quartoService.GetAll();
            var quarto = quartos.FirstOrDefault(q => q.StatusQuarto == (int)QuartoStatus.Disponivel);

            return new Reserva
            {
                DataEntrada = dataInicial,
                DataSaida = dataInicial.AddDays(qtdDias),
                Hospedes = new() { user },
                Quarto = quarto
            };
        }
    }
}
