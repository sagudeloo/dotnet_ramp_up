using Microsoft.AspNetCore.Mvc;
using authentication_jwt.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace authentication_jwt.Controllers;

[ApiController]
[Route("/api/Auth")]
public class AuthController : ControllerBase
{
    DatabaseContext _databaseContext;
    ILogger<AuthController> _logger;
    IConfiguration _configuration;

    class JwtConfig
    {
        public String Key { get; set; }
        public String Issuer { get; set; }
        public String Audience { get; set; }
        public String Subject { get; set; }
    }
    JwtConfig? _jwt;

    public AuthController(ILogger<AuthController> logger, DatabaseContext databaseContext, IConfiguration configuration)
    {
        (_logger, _databaseContext, _configuration) = (logger, databaseContext, configuration);
        _jwt = _configuration.GetSection("Jwt").Get<JwtConfig>();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public dynamic auth(string email, string password)
    {
        try
        {
            var user = (from u in _databaseContext.Users
                        where u.Email.Equals(email)
                        select u).FirstOrDefault();
            if (!user.Password.Equals(password))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("IsActiveRole", user.IsActiveRole.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtTokenConfig = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: singIn
            );

            string jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtTokenConfig);

            return Ok(new AuthenticatedResponse(){ Token = jwtTokenString });

        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}