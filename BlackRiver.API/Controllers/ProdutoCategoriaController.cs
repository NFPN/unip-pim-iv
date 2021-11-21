using BlackRiver.Data;
using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackRiver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoCategoriaController : ControllerBase
    {
        private readonly GenericDataService<ProdutoCategoria> service = new(new BlackRiverDBContextFactory());


        [HttpGet]
        public async Task<IEnumerable<ProdutoCategoria>> Get()
        {
            return await service.GetAllData();
        }

        [HttpGet("{id}")]
        public async Task<ProdutoCategoria> Get(int id)
        {
            return await service.GetData(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoCategoria categoria)
        {
            try
            {
                var result = await service.CreateData(categoria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var result = await service.UpdateData(id, new ProdutoCategoria
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
        public async Task<IActionResult> Delete(int id)
        {
            if (await service.DeleteData(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
