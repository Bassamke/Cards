using System.ComponentModel.DataAnnotations;

namespace Cards.DTOs;
public class CreateCardDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    [RegularExpression(@"^#[a-zA-Z0-9]{6}$", ErrorMessage = "The format should be 6 alphanumeric characters prefixed with a #.")]
    public string? Color { get; set; }
}
