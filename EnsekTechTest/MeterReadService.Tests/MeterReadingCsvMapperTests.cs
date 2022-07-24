using FluentAssertions;
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
    public class MeterReadingCsvMapperTests
    {
        private static MeterReadingCsvMapper _mapper;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _mapper = new();
        }

        [TestMethod]
        public void NonInt_AccountNumber_ThrowsException()
        {
            var input = new List<string> { "Test", "01/01/2022 10:15", "12345" };
            var a = () => _mapper.ColumnMapping(input);

            a.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void BadDateFormat_ThrowsException()
        {
            var input = new List<string> { "1234", "41/41/2022 10:15", "12345" };
            var a = () => _mapper.ColumnMapping(input);

            a.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void NonInt_Reading_ThrowsException()
        {
            var input = new List<string> { "1234", "01/01/2022 10:15", "Test" };
            var a = () => _mapper.ColumnMapping(input);

            a.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void ValidValues_Returns_MeterReading()
        {
            var input = new List<string> { "1234", "01/01/2022 10:15", "23456" };

            var result = _mapper.ColumnMapping(input);

            result.Should().BeOfType<MeterReading>();
            result.AccountId.Should().Be(1234);
            result.ReadingTime.Should().Be(new DateTime(2022, 01, 01, 10, 15, 0));
            result.ReadingValue.Should().Be(23456);
        }
    }
}
