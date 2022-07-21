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
        public async Task MeterReadingUploads_PassNullFileSet_ReturnsServerError()
        {
            _controller.ControllerContext.HttpContext = ConfigureFiles(null);

            var result = await _controller.MeterReadingUploads();

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task MeterReadingUploads_PassZeroFiles_ReturnsBadRequest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public async Task MeterReadingUpload_PassMultipleFiles_ReturnsBadRequest()
        {
            Assert.Fail();
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
