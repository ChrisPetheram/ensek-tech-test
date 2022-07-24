using MeterReadService.Abstractions;
using MeterReadService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeterReadApi.Api
{
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private IBulkUpload _bulkUploadService;

        public MeterReadingController(IBulkUpload bulkUploadService)
        {
            _bulkUploadService = bulkUploadService;
        }

        [HttpPost]
        [Route("~/meter-reading-uploads")]
        public async Task<IActionResult> MeterReadingUploads()
        {
            try
            {
                // Require exactly 1 file or throw a BadRequest response
                var file = Request.Form.Files.Single();

                var results = _bulkUploadService.ParseFile(file.OpenReadStream());

                return Ok(results);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
