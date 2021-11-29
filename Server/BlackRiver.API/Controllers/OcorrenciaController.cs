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
    public class OcorrenciaController : Controller
    {

        #region Workers

        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IEnumerable<Ocorrencia>> Get()
        {
            return await DataServices.OcorrenciaService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<Ocorrencia> Get(int id)
        {
            return await DataServices.OcorrenciaService.Get(id);
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Post([FromBody] Ocorrencia ocorrencia)
        {
            try
            {
                var result = await DataServices.OcorrenciaService.Create(ocorrencia);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Ocorrencia ocorrencia)
        {
            try
            {
                var result = await DataServices.OcorrenciaService.Update(id, ocorrencia);
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
            if (await DataServices.OcorrenciaService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }

        #endregion Workers


        #region Customer

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CustomerPost([FromBody] Ocorrencia ocorrencia)
        {
            try
            {
                var result = await DataServices.OcorrenciaService.Create(ocorrencia);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
