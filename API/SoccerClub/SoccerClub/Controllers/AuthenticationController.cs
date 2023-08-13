using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoccerClub.Services;
using SoccerClub.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Data;

namespace SoccerClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _iAuthenticationService;
        public AuthenticationController(IAuthenticationService iAuthenticationService)
        {
            _iAuthenticationService = iAuthenticationService;
        }
        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(TokenVM token)
        {
            string msg = _iAuthenticationService.CheckToken(token, out object result);
            if(msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM login)
        {
            string msg = string.Empty;
            if (login == null)
            {
                ModelState.AddModelError("NotFoundLogin", "Login is null!");
                return Ok(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return Ok(new ApiResponseVM
                {
                    Success = false,
                    Error = "Invalid username/password"
                });
            }
            msg = _iAuthenticationService.Login(login, out TokenVM UserToken);
            if (msg.Length > 0) return BadRequest(msg);


            return Ok(new ApiResponseVM
            {
                Success = false,
                Error = "Invalid username/password",
                data = UserToken
            });

        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterVM register)
        {
            string msg = string.Empty;
            if (register == null)
            {
                ModelState.AddModelError("NotFoundRegister", "Register is null!");
                return Ok(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(msg);
            }
            msg = _iAuthenticationService.Register(register);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }
        

    }
   
    
}
