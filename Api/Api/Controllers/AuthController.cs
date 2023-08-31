using Api.Models;
using Api.Request;
using Api.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration configuration;
        public AuthController(AppDbContext dbContext, IConfiguration configuration) {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SetToken(AuthRequest request) {
            var username = await dbContext.TbUsers.Where(u => u.Username == request.Username).FirstOrDefaultAsync();
            if(username==null)
                return NotFound();

            if (username.Password == setSHA(request.Password)) { 
                DateTime date = DateTime.Now.AddMinutes(10);
                return Ok(new AuthResponse()
                {
                    Token = GetToken(username, date),
                    Exp = date
                });
            }

            else
            {
                return BadRequest();
            }
        }

        private string GetToken(TbUser user, DateTime exp) { 
            var role=string.Empty;
            if (user.Level == 0)
                role = "Admin";
            if (user.Level == 1)
                role = "User";

            List<Claim> claims=new List<Claim>() { 
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                expires: exp,
                claims: claims,
                signingCredentials: cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private string setSHA(string s) { 
            StringBuilder stringBuilder = new StringBuilder();
            using (var sha=SHA256.Create())
            {
                var baytes = sha.ComputeHash(Encoding.UTF8.GetBytes(s));
                for (int i = 0; i < baytes.Length; i++)
                {
                    stringBuilder.Append(baytes[i].ToString("x2"));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
