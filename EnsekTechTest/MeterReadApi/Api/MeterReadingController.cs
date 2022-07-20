using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeterReadApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        [HttpPost]
        [Route("meter-reading-uploads")]
        public HttpResponseMessage MeterReadingUploads()
        {
            try
            {
                var file = Request.Form.Files.SingleOrDefault();


                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                };

            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
