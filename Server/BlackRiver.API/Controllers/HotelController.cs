using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackRiver.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IEnumerable<Hotel>> Get()
        {
            return await DataServices.HotelService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<Hotel> Get(int id)
        {
            return await GetHotel(id);
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Post([FromBody] Hotel hotel)
        {
            try
            {
                var result = await DataServices.HotelService.Create(hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Hotel hotel)
        {
            try
            {
                var result = await DataServices.HotelService.Update(id, hotel);
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
            if (await DataServices.HotelService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }

        private async Task<Hotel> GetHotel(int id)
        {
            using var context = DataServices.HotelService.ContextFactory.CreateDbContext();

            return await context
                .Set<Hotel>()
                .Include(h => h.MunicipioAtual)
                .Include(h => h.VagasEstacionamento)
                .Include(h => h.Quartos)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
