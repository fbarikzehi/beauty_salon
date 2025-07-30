using Microsoft.AspNetCore.Http;

namespace Beauty.Application.Modules.Setting.ViewModel
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string ServerPath { get; set; }
        public IFormFile File { get; set; }
    }
}
