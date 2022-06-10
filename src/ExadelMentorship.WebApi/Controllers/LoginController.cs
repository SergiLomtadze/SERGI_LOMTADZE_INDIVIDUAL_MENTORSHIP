using ExadelMentorship.WebApi.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [HttpPost("token/{userName}/{password}")]
        public async Task<IActionResult> GetTokenAsync([FromRoute] string userName, [FromRoute] string password)
        {
            var result = await _tokenService.GetToken(userName, password);
            return Ok($"token: {result}");
        }
    }
}
