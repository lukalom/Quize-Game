using Application.Services.User_Management;
using Application.Services.User_Management.Dto;
using Application.Services.User_Management.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string userName)
        {
            var result = await _userService.CreateUser(userName);
            if (result.IsSuccess)
            {
                return Ok(result.Content);
            }

            return BadRequest(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromQuery] UpdateUserDto requestDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUser(requestDto);
                if (result.IsSuccess)
                {
                    return Ok(result.Content);
                }

                return BadRequest(result.Error);
            }

            return BadRequestModelState();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result.Content)
            {
                return Ok(result.Content);
            }

            return BadRequest(result.Error);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> DisableUser(int id)
        {
            var result = await _userService.DisableUser(id);
            if (result.Content)
            {
                return Ok(result.Content);
            }

            return BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] FilterParameters filterQuery)
        {
            var result = await _userService.GetAll(filterQuery);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Error);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> CheckIfUserExists(string userName)
        {
            return Ok(await _userService.CheckIfUserExists(userName));
        }

        private IActionResult BadRequestModelState()
        {
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new { error = errorMessages });
        }
    }
}
