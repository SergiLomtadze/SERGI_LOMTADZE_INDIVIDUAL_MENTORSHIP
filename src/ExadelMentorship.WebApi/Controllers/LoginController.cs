using ExadelMentorship.WebApi.Token;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] UserInfo userInfo)
        {
            var result = await _tokenService.GetToken(userInfo.UserName, userInfo.Password);
            return Ok($"token: {result}");
        }
    }
}
