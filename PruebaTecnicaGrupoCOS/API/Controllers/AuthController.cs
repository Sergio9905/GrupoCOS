using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Interfaces;
using PruebaTecnicaGrupoCOS.Application.Services;
using System.Threading.Tasks;

namespace PruebaTecnicaGrupoCOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            try
            {
                var (jwtToken, refreshToken) = await _authService.AuthenticateUser(loginRequest.Email, loginRequest.Password, HttpContext.Connection.RemoteIpAddress.ToString());
                return Ok(new { Token = jwtToken, RefreshToken = refreshToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto request)
        {
            try
            {
                var newJwtToken = await _authService.RefreshJwtToken(request.RefreshToken, HttpContext.Connection.RemoteIpAddress.ToString());
                return Ok(new { Token = newJwtToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}