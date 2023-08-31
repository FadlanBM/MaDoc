using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDocController : ControllerBase
    {
        public readonly AppDbContext dbContext;
        public GetDocController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> getData(string id) { 
            var data =await dbContext.TbDokumen.Where(d=>d.IdDokumen==Convert.ToInt32(id)).FirstOrDefaultAsync();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }            
        }
    }
}
