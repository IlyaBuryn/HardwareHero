using Microsoft.AspNetCore.Http;

namespace Storage.DTOs.Models
{
    public class UploadFileModel
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }
}
