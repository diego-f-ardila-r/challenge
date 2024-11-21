using Metafar.Challenge.Model.Configurations;

namespace Metafar.Challenge.Model;

public record AppSettingsModel
{
    public SwaggerConfigurationModel? JwtToken { get; set; }
    public SwaggerConfigurationModel? Swagger { get; set; }
}