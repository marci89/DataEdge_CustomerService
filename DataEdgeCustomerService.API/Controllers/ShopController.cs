﻿using DataEdge_CustomerService.Business.Models;
using DataEdge_CustomerService.Business.Models.Request.Shop;
using DataEdge_CustomerService.Business.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DataEdgeCustomerService.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly ShopService _service;

        public ShopController(ShopService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.ReadById(id);
            if (!String.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        /// <summary>
        /// List
        /// </summary>
        [HttpGet("List")]
        public async Task<IActionResult> List([FromQuery] ListShopRequest request)
        {

            var response = await _service.List(request);
            if (!String.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }


        /// <summary>
        /// Create
        /// </summary>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateShopRequest request)
        {

            var response = await _service.Create(request);
            if (!String.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(response.ErrorMessage);
            }

            return CreatedAtAction("Create", new { id = response.Result.Id }, response.Result);
        }

        /// <summary>
        /// Update
        /// </summary>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateShopRequest request)
        {
            var response = await _service.Update(request);

            if (!String.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }

}

   