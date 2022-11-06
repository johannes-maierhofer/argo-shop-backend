using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Argo.Shop.Application.Common.Identity;
using Argo.Shop.Application.Features;
using Argo.Shop.WebApi.Configuration;

// see also JwtAuthSampleAPI from
// https://github.com/manoj-choudhari-git/AspNetCore-Identity/blob/master/JwtAuthSample/JwtAuthSampleAPI/Controllers/AuthController.cs

namespace Argo.Shop.WebApi.Controllers.Identity
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;

        public AuthenticationController(
            IOptions<JwtBearerTokenSettings> jwtTokenOptions,
            IIdentityService identityService)
        {
            _identityService = identityService;
            _jwtBearerTokenSettings = jwtTokenOptions.Value;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            var validationResult = await _identityService.ValidateUserAsync(credentials.Username, credentials.Password);

            if (validationResult.Status == ResultStatus.Success && validationResult.Data != null)
            {
                var token = GenerateToken(validationResult.Data);
                return Ok(new { Token = token, Message = "Success" });
            }

            return new BadRequestObjectResult(new
            {
                Message = validationResult.Messages.FirstOrDefault() ?? "Login failed"
            });
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            // Well, What do you want to do here ?
            // Wait for token to get expired OR 
            // Maintain token cache and invalidate the tokens after logout method is called
            return Ok(new { Token = "", Message = "Logged Out" });
        }

        private string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddSeconds(_jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSettings.Audience,
                Issuer = _jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginCredentials
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}