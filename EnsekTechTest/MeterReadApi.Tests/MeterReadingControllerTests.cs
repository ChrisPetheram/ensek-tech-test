using MeterReadApi.Api;
using MeterReadService.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadApi.Tests
{
    [TestClass]
    public class MeterReadingControllerTests
    {
        private MeterReadingController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var bulkUploadServiceMock = new Mock<MeterReadBulkUpload>();
            _controller = new MeterReadingController(bulkUploadServiceMock.Object);
        }

        [TestMethod]
        public async Task MeterReadingUploads_PassNullFileSet_ReturnsServerError()
        {
            Assert.Fail();
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

    }
}
