using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EMS.Auth.Configurations;
using EMS.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EMS.Services;

public class AuthService
{
    private readonly JwtBearerConfiguration _jwtConfiguration;
    
    public AuthService(IOptions<GeneralConfiguration> configuration)
    {
        _jwtConfiguration = configuration.Value.JwtBearerConfiguration;
    }
    
    public dynamic GetJwt()
    {
        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            notBefore: now,
            expires: now.Add(TimeSpan.FromMinutes(_jwtConfiguration.ExpiresMinutes)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfiguration.SecurityKey)),
                SecurityAlgorithms.HmacSha256)
            );
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return new
        {
            accessToken = encodedJwt
        };
    }
}
