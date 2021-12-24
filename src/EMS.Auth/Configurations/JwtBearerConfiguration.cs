namespace EMS.Auth.Configurations;

public class JwtBearerConfiguration
{
    public string Issuer { get; set; }
    
    public string Audience { get; set; }

    public int ExpiresInMinutes { get; set; }
    
    public string SecurityKey { get; set; }
}
