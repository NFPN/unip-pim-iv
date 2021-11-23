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
    public class ProdutoController : Controller
    {
        private readonly GenericDataService<Produto> service = new(new BlackRiverDBContextFactory());


        [HttpGet]
        public async Task<IEnumerable<Produto>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Produto> Get(int id)
        {
            return await service.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            try
            {
                var result = await service.Create(produto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
        {
            try
            {
                var result = await service.Update(id, produto);
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
