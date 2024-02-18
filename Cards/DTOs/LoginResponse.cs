namespace Cards.DTOs
{
    public class LoginResponse
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string Role { get; set; }
        public List<string>? Roles { get; set; }
        public DateTime Tokenxpiry { get; set; }
    }
}
