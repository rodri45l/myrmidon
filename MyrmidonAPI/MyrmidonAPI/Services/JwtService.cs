using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace MyrmidonAPI.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public string CreateTokenAsync(User user)
    {
        var jwtConfig = _config.GetSection("jwtConfig");
        // Create claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            // Add additional claims as necessary
        };

        // Create the security key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Secret"]));

        // Create the credentials used to generate the token
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(Convert.ToDouble(jwtConfig["ExpiresIn"])),
            SigningCredentials = credentials,
            Audience = jwtConfig["validAudience"],
            Issuer = jwtConfig["validIssuer"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}

