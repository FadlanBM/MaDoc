using Microsoft.AspNetCore.Mvc;

namespace Api.Request
{
    public class AuthRequest 
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
