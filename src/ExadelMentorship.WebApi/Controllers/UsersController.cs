using ExadelMentorship.Auth.Interfaces;
using ExadelMentorship.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequest request)
        {
            var result = await _userService.GetTokenAsync(request);
            return Ok(result);
        }
    }
}
