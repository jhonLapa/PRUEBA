using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Product.Request;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListProduct([FromBody] BaseFiltersRequest filters)
        {
            var response = await _productApplication.ListProduct(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectProduct()
        {
            var response = await _productApplication.ListSelectProduct();
            return Ok(response);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var response = await _productApplication.ProductById(productId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProduct([FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.RegisterProduct(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{productId:int}")]
        public async Task<IActionResult> EditProduct(int productId, [FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.EditProduct(productId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{productId:int}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var response = await _productApplication.RemoveProduct(productId);
            return Ok(response);
        }

        [HttpDelete("Remove2/{productId:int}")]
        public async Task<IActionResult> RemoveProduct2(int productId)
        {
            var response = await _productApplication.RemoveProduct2(productId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
