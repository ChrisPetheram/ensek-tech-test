using FluentAssertions;
using MeterReadApi.Api;
using MeterReadService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadApi.Tests
{
    [TestClass]
    public class MeterReadingControllerTests
    {
        private static MeterReadingController _controller;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var bulkUploadServiceMock = new Mock<MeterReadBulkUpload>();
            _controller = new MeterReadingController(bulkUploadServiceMock.Object);
            _controller.ControllerContext = new ControllerContext();
        }

        [TestMethod]
        public async Task MeterReadingUploads_PassNullFileSet_ReturnsBadRequest()
        {
            _controller.ControllerContext.HttpContext = ConfigureFiles(null);

            var result = await _controller.MeterReadingUploads();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task MeterReadingUploads_PassZeroFiles_ReturnsBadRequest()
        {
            var files = new FormFileCollection();
            _controller.ControllerContext.HttpContext = ConfigureFiles(files);

            var result = await _controller.MeterReadingUploads();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task MeterReadingUpload_PassMultipleFiles_ReturnsBadRequest()
        {
            var files = new FormFileCollection();
            files.Add(CreateFile("File1"));
            files.Add(CreateFile("File2"));
            _controller.ControllerContext.HttpContext = ConfigureFiles(files);

            var result = await _controller.MeterReadingUploads();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private FormFile CreateFile(string fileContents)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents));
            var file = new FormFile(stream, 0, 0, "MeterReadings", "Meter_Readings.csv");

            return file;
        }

        private HttpContext ConfigureFiles(IFormFileCollection files)
        {
            var context = new DefaultHttpContext();

            context.Request.Headers.Add("Content-Type", "multipart/form-data");
            context.Request.Form = new FormCollection(null, files);

            return context;
        }

    }
}
