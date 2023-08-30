using Api.Models;
using Api.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public RegisterController(AppDbContext dbContext) { 
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostRegister(RegisterRequest request) {

            if (request.Username.Length == 0 || request.Name.Length == 0 || request.Password.Length == 0)
                return BadRequest("Form belum terisi semua");
            var identity = await dbContext.TbIdentitas.Where(i => i.NameIdentitas == request.IdIdentitas).FirstOrDefaultAsync();
            if (identity == null)
                return NotFound("Identitas Not Found");

            TbUser user= new TbUser();
            user.Name = request.Name;

            user.Username= request.Username;
            user.NoIdentitas = request.NoIdentitas;
            user.IdIdentitas= identity.IdIdentitas;
            user.Password = setSHA(request.Password);
            user.Verify = 0;
            user.Level = 1;
            user.CreatedAt= DateTime.Now;
            await dbContext.TbUsers.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok("Berhasil Input data");
        }

        private string setSHA(string s) { 
            StringBuilder sb=new StringBuilder();
            using (var sha=SHA256.Create())
            {
                var baytes=sha.ComputeHash(Encoding.UTF8.GetBytes(s));
                for (int i = 0; i < baytes.Length; i++)
                {
                    sb.Append(baytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
