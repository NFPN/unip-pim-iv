using BlackRiver.Data;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HospedeController : Controller
    {
        private readonly GenericDataService<Hospede> service = new(new BlackRiverDBContextFactory());

        #region Hotel workers

        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IEnumerable<Hospede>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<Hospede> Get(int id)
        {
            return await service.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Hospede hospede)
        {
            try
            {
                if (string.IsNullOrEmpty(hospede.Login.Username) && string.IsNullOrEmpty(hospede.Login.Password))
                    return BadRequest("Para cadastrar passe as credenciais");

                var result = await service.Create(hospede);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Hospede hospede)
        {
            try
            {
                var result = await service.Update(id, hospede);
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
            if (await service.Delete(id))
                return Ok();

            return BadRequest("Objeto não existe");
        }

        #endregion Hotel workers

        #region Customer

        //[HttpPost]
        //[Route("token")]
        //public async Task<IActionResult> CustomerPost([FromBody] Hospede hospede)
        //{
        //    try
        //    {
        //        var result = await service.Create(hospede);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPut]
        [Route("token")]
        public async Task<IActionResult> CustomerPut([FromBody] Hospede hospede)
        {
            try
            {
                var id = await GetUserID();
                var result = await service.Update(id, hospede);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("token")]
        public async Task<Hospede> CustomerGet()
        {
            var id = await GetUserID();
            return await service.Get(id);
        }

        private async Task<int> GetUserID()
        {
            var all = await service.GetAll();

            if (all.Any(h => h.Email.Equals(User.Identity.Name)))
                return all.FirstOrDefault(h => h.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase)).Id;

            return all.FirstOrDefault(h => h.Nome.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase)).Id;
        }

        #endregion Customer
    }
}
