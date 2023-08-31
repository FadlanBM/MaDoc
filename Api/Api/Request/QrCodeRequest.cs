using Microsoft.AspNetCore.Mvc;

namespace Api.Request
{
    public class QrCodeRequest
    {
        public string TokenDokumen { get; set; } = null!;
    }
}
