﻿using Api.Models;
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
    public class QrValidationController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public QrValidationController(AppDbContext dbContext) { 
            this.dbContext= dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> getTokenDoc(QrCodeRequest request) {
          
            var data =await dbContext.TbDokumen.Where(d=>d.TokenDokumen == request.TokenDokumen).FirstOrDefaultAsync();
            if (data != null)
            {
                return Ok(data.IdDokumen);
            }
            return BadRequest();
        }

        private string getToken(string s) { 
            StringBuilder sb=new StringBuilder();
            using (var sha=SHA256.Create())
            {
                var baytes = sha.ComputeHash(Encoding.UTF8.GetBytes(s));
                for (int i = 0; i < baytes.Length; i++)
                {
                    sb.Append(baytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
