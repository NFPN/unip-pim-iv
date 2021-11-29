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
    public class VagasEstacionamentoController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<VagaEstacionamento>> Get()
        {
            return await DataServices.VagaEstacionamentoService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<VagaEstacionamento> Get(int id)
        {
            return await DataServices.VagaEstacionamentoService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VagaEstacionamento vagas)
        {
            try
            {
                var result = await DataServices.VagaEstacionamentoService.Create(vagas);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VagaEstacionamento vaga)
        {
            try
            {
                var result = await DataServices.VagaEstacionamentoService.Update(id, vaga);
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
            if (await DataServices.VagaEstacionamentoService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
