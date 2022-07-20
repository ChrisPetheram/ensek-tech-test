using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeterReadApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        [HttpPost]
        [Route("meter-reading-uploads")]
        public void MeterReadingUploads([FromBody] string value)
        {
        }
    }
}
