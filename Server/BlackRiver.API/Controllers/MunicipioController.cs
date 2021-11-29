﻿using BlackRiver.Data.Services;
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
    public class MunicipioController:Controller
    {


        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IEnumerable<Municipio>> Get()
        {
            return await DataServices.MunicipioService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<Municipio> Get(int id)
        {
            return await DataServices.MunicipioService.Get(id);
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Post([FromBody] Municipio municipio)
        {
            try
            {
                var result = await DataServices.MunicipioService.Create(municipio);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> Put(int id, [FromBody] Municipio municipio)
        {
            try
            {
                var result = await DataServices.MunicipioService.Update(id, municipio);
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
            if (await DataServices.MunicipioService.Delete(id))
                return Ok();

            return BadRequest("Item does't exist");
        }
    }
}
