using EMS.Auth.Configurations;
using EMS.DAL.Configurations;

namespace EMS.Configurations;

public class GeneralConfiguration
{
    public DatabaseConfiguration DatabaseConfiguration { get; set; }
    
    public JwtBearerConfiguration JwtBearerConfiguration { get; set; }
}
