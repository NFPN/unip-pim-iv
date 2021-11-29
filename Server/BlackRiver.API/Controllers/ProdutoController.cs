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
    public class ProdutoController : Controller
    {

        [HttpGet]
        [Authorize(Roles = "employee, manager")]
        public async Task<IEnumerable<Produto>> Get()
        {
            return await DataServices.ProdutoService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee, manager")]
        public async Task<Produto> Get(int id)
        {
            return await DataServices.ProdutoService.Get(id);
        }

        [HttpPost]
        [Authorize(Roles = "employee, manager")]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            try
            {
                var result = await DataServices.ProdutoService.Create(produto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "employee, manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
        {
            try
            {
                var result = await DataServices.ProdutoService.Update(id, produto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "employee, manager")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await DataServices.ProdutoService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
