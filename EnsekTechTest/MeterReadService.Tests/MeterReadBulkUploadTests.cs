using FluentAssertions;
using MeterReadDatabaseAccess;
using MeterReadEntities;
using MeterReadService.Services;
using Moq;

namespace MeterReadService.Tests
{
    [TestClass]
    public class MeterReadBulkUploadTests
    {
        private static MeterReadBulkUpload _service;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var repository = new Mock<IRepository<MeterReading>>();
            _service = new MeterReadBulkUpload(repository.Object);
        }

        [TestMethod]
        public void ParseFile_NullStream_ThrowsException()
        {
            Action action = () => _service.ParseFile(null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}