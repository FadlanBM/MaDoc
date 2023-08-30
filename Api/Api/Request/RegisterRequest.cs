using Microsoft.AspNetCore.Mvc;

namespace Api.Request
{
    public class RegisterRequest 
    {
        public string Name { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? NoIdentitas { get; set; }

        public string IdIdentitas { get; set; }=string.Empty;
    }
}
