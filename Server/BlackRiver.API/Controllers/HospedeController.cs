using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackRiver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospedeController : Controller
    {
        #region Hotel workers

        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IEnumerable<Hospede>> Get()
        {
            return await DataServices.HospedeService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<Hospede> Get(int id)
        {
            return await DataServices.HospedeService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Hospede hospede)
        {
            try
            {
                if (string.IsNullOrEmpty(hospede.Email))
                    return BadRequest("Email inválido");

                var all = await DataServices.HospedeService.GetAll();

                if (all.Any(a => a.Email == hospede.Email))
                    throw new Exception("Usuário já existe");

                var result = await DataServices.HospedeService.Create(hospede);
                return Ok("Hóspede criado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Hospede hospede)
        {
            try
            {
                var result = await DataServices.HospedeService.Update(id, hospede);
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
            if (await DataServices.HospedeService.Delete(id))
                return Ok();

            return BadRequest("Objeto não existe");
        }

        #endregion Hotel workers

        #region Customer

        [HttpPut]
        [Route("token")]
        [Authorize]
        public async Task<IActionResult> CustomerPut([FromBody] Hospede hospede)
        {
            try
            {
                var id = await GetUserID();
                var result = await DataServices.HospedeService.Update(id, hospede);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("token")]
        [Authorize]
        public async Task<Hospede> CustomerGet()
        {
            var id = await GetUserID();
            return await DataServices.HospedeService.Get(id);
        }

        private async Task<int> GetUserID()
        {
            var all = await DataServices.HospedeService.GetAll();

            if (all.Any(h => h.Email.Equals(User.Identity.Name)))
                return all.FirstOrDefault(h => h.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase)).Id;

            return all.FirstOrDefault(h => h.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase)).Id;
        }

        #endregion Customer
    }
}
