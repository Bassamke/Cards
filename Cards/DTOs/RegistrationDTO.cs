using System.ComponentModel;

namespace Cards.DTOs;
public class RegistrationDTO
{
    public required string UserName {  get; set; } 
    public required string Password { get; set; }
    public required string Role { get; set; }


}
