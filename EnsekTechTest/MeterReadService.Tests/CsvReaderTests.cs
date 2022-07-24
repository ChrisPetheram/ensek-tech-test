using FluentAssertions;
using MeterReadService.Abstractions;
using MeterReadService.Models;
using MeterReadService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadService.Tests
{
    [TestClass]
    public class CsvReaderTests
    {
        private static CsvReader<MeterReading> _reader;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var mapper = new MeterReadingCsvMapper();
            _reader = new CsvReader<MeterReading>(mapper);
        }

        [TestMethod]
        public void GetRows_WithNullStream_ThrowsException()
        {
            var a = () => _reader.GetRows(null);
            a.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void GetRows_WithNullMapper_ThrowsException()
        {
            var reader = new CsvReader<MeterReading>(null);
            var a = () => reader.GetRows(new MemoryStream());

            a.Should().Throw<ArgumentException>();
        }
    }
}
