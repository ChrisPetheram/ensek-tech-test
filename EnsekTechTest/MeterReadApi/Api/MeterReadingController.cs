﻿using MeterReadService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeterReadApi.Api
{
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private MeterReadBulkUpload _bulkUploadService;

        public MeterReadingController(MeterReadBulkUpload bulkUploadService)
        {
            _bulkUploadService = bulkUploadService;
        }

        [HttpPost]
        [Route("~/meter-reading-uploads")]
        public async Task<HttpResponseMessage> MeterReadingUploads()
        {
            try
            {
                // Require exactly 1 file or throw a BadRequest response
                var file = Request.Form.Files.Single();

                var results = _bulkUploadService.ParseFile(file.OpenReadStream());

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                };

            }
            catch (InvalidOperationException)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
