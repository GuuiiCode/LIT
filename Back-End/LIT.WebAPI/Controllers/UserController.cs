using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LIT.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userServce)
        {
            _userService = userServce;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel userViewModel)
        {
            var result = await _userService.RegisterAsync(userViewModel);

            if (!result.Success)
                return BadRequest(result.Error);

            return Ok("User successfully registered");
        }

        [HttpPost]
        public async Task<ActionResult<LoginViewModel>> Login([FromBody] LoginViewModel loginViewModel)
        {
            var result = await _userService.LoginAsync(loginViewModel);

            if (!result.ResultViewModel.Success)
                return Unauthorized(result.ResultViewModel.Error);

            return Ok(result);
        }

    }
}
