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
    public class FuncionarioController : Controller
    {

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IEnumerable<Funcionario>> Get()
        {
            return await DataServices.FuncionarioService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<Funcionario> Get(int id)
        {
            return await DataServices.FuncionarioService.Get(id);
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Post([FromBody] Funcionario funcionario)
        {
            try
            {
                var result = await DataServices.FuncionarioService.Create(funcionario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var result = await DataServices.FuncionarioService.Update(id, new Funcionario
                {
                    Nome = value,
                });

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
            if (await DataServices.FuncionarioService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
