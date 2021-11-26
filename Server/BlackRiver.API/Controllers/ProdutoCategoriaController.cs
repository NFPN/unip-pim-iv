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
    public class ProdutoCategoriaController : ControllerBase
    {
        private readonly GenericDataService<ProdutoCategoria> service = new(new BlackRiverDBContextFactory());

        [HttpGet]
        [Authorize(Roles = "employee, manager")]
        public async Task<IEnumerable<ProdutoCategoria>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee, manager")]
        public async Task<ProdutoCategoria> Get(int id)
        {
            return await service.Get(id);
        }

        [HttpPost]
        [Authorize(Roles = "employee, manager")]
        public async Task<IActionResult> Post([FromBody] ProdutoCategoria produtoCategoria)
        {
            try
            {
                var result = await service.Create(produtoCategoria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "employee, manager")]
        public async Task<IActionResult> Put(int id, [FromBody] ProdutoCategoria produtoCategoria)
        {
            try
            {
                var result = await service.Update(id, produtoCategoria);
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
            if (await service.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
