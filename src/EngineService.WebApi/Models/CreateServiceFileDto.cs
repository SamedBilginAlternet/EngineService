using Microsoft.AspNetCore.Http;
namespace EngineService.WebApi.Models
{
    public class CreateServiceFileDto
    {
        public IFormFile ?File { get; set; }
    }
}
