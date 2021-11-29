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
    public class QuartoController : Controller
    {
        #region Workers

        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IEnumerable<Quarto>> Get()
        {
            return await DataServices.QuartoService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<Quarto> Get(int id)
        {
            return await DataServices.QuartoService.Get(id);
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Post([FromBody] Quarto quarto)
        {
            try
            {
                var result = await DataServices.QuartoService.Create(quarto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Quarto quarto)
        {
            try
            {
                var result = await DataServices.QuartoService.Update(id, quarto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await DataServices.QuartoService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }

        #endregion Workers

        [HttpGet]
        [Route("token")]
        public async Task<Quarto> CustomerGet()
        {
            try
            {
                var user = await GetUser();
                var reserva = user.Reservas.LastOrDefault();

                return await DataServices.QuartoService.Get(reserva.Quarto.Id);
            }
            catch
            {
                return null;
            }
        }

        private async Task<Hospede> GetUser()
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
    }
}
