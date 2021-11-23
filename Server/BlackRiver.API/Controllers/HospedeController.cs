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
    public class HospedeController : Controller
    {
        private readonly GenericDataService<Hospede> service = new(new BlackRiverDBContextFactory());

        [HttpGet]
        public async Task<IEnumerable<Hospede>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Hospede> Get(int id)
        {
            return await service.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Hospede hospede)
        {
            try
            {
                var result = await service.Create(hospede);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Hospede hospede)
        {
            try
            {
                var result = await service.Update(id, hospede);
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
