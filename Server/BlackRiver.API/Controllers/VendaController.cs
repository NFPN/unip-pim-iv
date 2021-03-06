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
    public class VendaController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<Venda>> Get()
        {
            return await DataServices.VendaService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Venda> Get(int id)
        {
            return await DataServices.VendaService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Venda venda)
        {
            try
            {
                var result = await DataServices.VendaService.Create(venda);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Venda venda)
        {
            try
            {
                var result = await DataServices.VendaService.Update(id, venda);
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
            if (await DataServices.VendaService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
