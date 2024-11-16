namespace Metafar.Challenge.Dto;
public record TokenDto
{
    public string? Type { get; set; } = "JWT";
    public string? Token { get; set; }
}