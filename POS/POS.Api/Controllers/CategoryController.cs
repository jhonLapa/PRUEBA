﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Category.Request;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListCategory([FromBody] BaseFiltersRequest filters)
        {       
            var response = await _categoryApplication.ListCategory(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectCategory()
        {
            var response = await _categoryApplication.ListSelectCategory();
            return Ok(response);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> CategoryById(int categoryId)
        {
            var response = await _categoryApplication.CategoryById(categoryId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCategory([FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.RegisterCategory(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{categoryId:int}")]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.EditCategory(categoryId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{categoryId:int}")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            var response = await _categoryApplication.RemoveCategory(categoryId);
            return Ok(response);
        }

        [HttpDelete("Remove2/{categoryId:int}")]
        public async Task<IActionResult> RemoveCategory2(int categoryId)
        {
            var response = await _categoryApplication.RemoveCategory2(categoryId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
