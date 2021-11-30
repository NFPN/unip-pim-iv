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
    public class FuncionarioController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IEnumerable<Funcionario>> Get()
        {
            return await DataServices.FuncionarioService.ContextFactory.CreateDbContext()
                .Set<Funcionario>()
                .Include(h => h.MunicipioId)
                .Include(h => h.HotelId)
                .Include(h => h.LoginId)
                .ToListAsync();
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
                var all = await DataServices.FuncionarioService.GetAll();

                if (all.Any(a => a.CTPS.Equals(funcionario.CTPS)))
                    return BadRequest("Funcionário já registrado");

                var result = await DataServices.FuncionarioService.Create(funcionario);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Funcionario funcionario)
        {
            try
            {
                var result = await DataServices.FuncionarioService.Update(id, funcionario);

                return Ok("Funcionário foi atualizado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
