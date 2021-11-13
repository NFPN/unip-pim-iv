using BlackRiver.Data;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlackRiver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoCategoriaController : ControllerBase
    {
        private readonly BlackRiverDBContextFactory factory = new();

        // GET: api/<ProdutoCategoriaController>
        [HttpGet]
        public IEnumerable<ProdutoCategoria> Get()
        {
            using var context = factory.CreateDbContext();
            return context.Categorias.ToList();
        }

        // GET api/<ProdutoCategoriaController>/5
        [HttpGet("{id}")]
        public ProdutoCategoria Get(int id)
        {
            using var context = factory.CreateDbContext();
            return context.Categorias.FirstOrDefault(c => c.Id == id);
        }

        // POST api/<ProdutoCategoriaController>
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            try
            {
                using var context = factory.CreateDbContext();
                context.Categorias.Add(new ProdutoCategoria
                {
                    Nome = value,
                });

                var result = context.SaveChanges();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<ProdutoCategoriaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            try
            {
                using var context = factory.CreateDbContext();
                var categoria = context.Categorias.FirstOrDefault(c => c.Id == id);

                if (categoria != null)
                {
                    categoria.Nome = value;
                    var result = context.SaveChanges();
                    return Ok(result);
                }

                return NotFound(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<ProdutoCategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                using var context = factory.CreateDbContext();

                var produtoToDelete = context.Categorias.FirstOrDefault(c => c.Id == id);

                if (produtoToDelete != null)
                {
                    var result = context.Categorias.Remove(produtoToDelete);
                    context.SaveChanges();
                    return Ok(result);
                }

                return NotFound(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
