using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocListController : ControllerBase
    {

        private readonly AppDbContext dbContext;
        public DocListController(AppDbContext dbContext) { 
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetData() {           
            var data =await (from h in dbContext.TbHistories
                             join d in dbContext.TbDokumen
                             on h.IdDokumen equals d.IdDokumen      
                             join u in dbContext.TbUsers
                             on h.IdUser equals u.IdUser
                             where h.IdUser==int.Parse(User.FindFirstValue(ClaimTypes.SerialNumber))

                             select new { 
                                nameDoc=d.NameDokumen,
                                namaPenerimaPertama=d.PenerimaPertama,
                                namapenerima=u.Name
                             }).ToListAsync();
               return Ok(data);

        }
    }
}
