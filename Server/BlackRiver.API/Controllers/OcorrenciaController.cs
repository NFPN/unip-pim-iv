﻿using BlackRiver.Data;
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
    public class OcorrenciaController:Controller
    {
        private readonly GenericDataService<Ocorrencia> service = new(new BlackRiverDBContextFactory());


        [HttpGet]
        public async Task<IEnumerable<Ocorrencia>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Ocorrencia> Get(int id)
        {
            return await service.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ocorrencia ocorrencia)
        {
            try
            {
                var result = await service.Create(ocorrencia);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Ocorrencia ocorrencia)
        {
            try
            {
                var result = await service.Update(id, ocorrencia);
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
