namespace Metafar.Challenge.Dto;
public record TokenDto
{
    public string? Type { get; set; } = "Bearer";
    public string? Token { get; set; }
}