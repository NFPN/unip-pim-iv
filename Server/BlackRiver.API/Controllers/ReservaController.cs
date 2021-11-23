using BlackRiver.Data;
using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackRiver.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : Controller
    {
        private readonly GenericDataService<Reserva> service = new(new BlackRiverDBContextFactory());

        [HttpGet]
        public async Task<IEnumerable<Reserva>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Reserva> Get(int id)
        {
            return await service.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reserva reserva)
        {
            try
            {
                var result = await service.Create(reserva);
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
                var result = await service.Update(id, reserva);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await service.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
