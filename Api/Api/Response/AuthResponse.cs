using Microsoft.AspNetCore.Mvc;

namespace Api.Response
{
    public class AuthResponse 
    {
        public string Token { get; set; }
        public DateTime Exp { get; set; }
    }
}
