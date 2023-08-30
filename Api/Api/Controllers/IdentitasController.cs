using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitasController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        public IdentitasController(AppDbContext dbContext) { 
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> getIdentitas() { 
            var data=await(from i in dbContext.TbIdentitas
                           select new { 
                                    nama=i.NameIdentitas
                           }).ToListAsync();
            return Ok(data); 
        }
    }
}
