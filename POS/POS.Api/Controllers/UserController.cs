using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.User.Request;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Generate/Token")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequestDto requestDto)
        {
            var response = await _userApplication.GenerateToken(requestDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);

        }


        [HttpPost]
        public async Task<IActionResult> ListUser([FromBody] BaseFiltersRequest filters)
        {
            var response = await _userApplication.ListUser(filters);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> UserById(int userId)
        {
            var response = await _userApplication.UserById(userId);
            return Ok(response);
        }



        [HttpPut("Edit/{userId:int}")]
        public async Task<IActionResult> EditUser(int userId, [FromForm] UserRequestDto requestDto)
        {
            var response = await _userApplication.EditUser(userId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{userId:int}")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            var response = await _userApplication.RemoveUser(userId);
            return Ok(response);
        }

        [HttpDelete("Remove2/{userId:int}")]
        public async Task<IActionResult> RemoveUser2(int userId)
        {
            var response = await _userApplication.RemoveUser2(userId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
