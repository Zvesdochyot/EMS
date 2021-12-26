using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using EMS.Auth.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace EMS.Auth.Validators;

public class JwtBearerValidator : JwtSecurityTokenHandler
{
    private readonly JwtBearerConfiguration _jwtConfiguration;
    
    public JwtBearerValidator(JwtBearerConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
    }

    public override ClaimsPrincipal ValidateToken(
        string token, 
        TokenValidationParameters validationParameters,
        out SecurityToken validatedToken)
    {
        validationParameters.ValidateIssuer = true;
        validationParameters.ValidateAudience = true;
        validationParameters.ValidateLifetime = true;
        validationParameters.ValidateIssuerSigningKey = true;
        validationParameters.ValidIssuer = _jwtConfiguration.Issuer;
        validationParameters.ValidAudience = _jwtConfiguration.Audience;
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfiguration.SecurityKey));

        try
        {
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw new AuthenticationException();
        }
    }
}
