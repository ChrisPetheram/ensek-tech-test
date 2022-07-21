using FluentAssertions;
using MeterReadService.Services;

namespace MeterReadService.Tests
{
    [TestClass]
    public class MeterReadBulkUploadTests
    {
        private static MeterReadBulkUpload _service;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _service = new MeterReadBulkUpload();
        }

        [TestMethod]
        public void ParseFile_NullStream_ThrowsException()
        {
            Action action = () => _service.ParseFile(null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}