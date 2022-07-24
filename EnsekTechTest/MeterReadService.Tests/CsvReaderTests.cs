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
        private CsvReader<MeterReading> _reader;

        [TestInitialize]
        public void TestInitialize()
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

        [TestMethod]
        public void GetRows_WithGoodValues_ReturnsObjects()
        {
            var input = new List<string>
            {
                "123, 01/01/2022 11:00, 456",
                "234, 02/01/2022 12:00, 567"
            };
            using (var stream = GetMockFileStream(input))
            {
                var (good, bad) = _reader.GetRows(stream);

                bad.Should().BeEmpty();
                good.Count.Should().Be(2);

                for (int i = 0; i < 2; i++)
                {
                    Assert.AreEqual(input.ElementAt(i), ReconstructString(good.ElementAt(i)));
                }
            }
        }

        [TestMethod]
        public void GetRows_WithBadValues_ReturnsFailedRows()
        {
            var input = new List<string>
            {
                "123x, 01/01/2022x11:00, x456",
                "234x, 02/01/2022 12:00, 567"
            };
            using (var stream = GetMockFileStream(input))
            {
                var (good, bad) = _reader.GetRows(stream);

                bad.Count.Should().Be(2);
                bad.Should().BeEquivalentTo(input);
            }
        }

        [TestMethod]
        public void GetRows_WithGoodAndBadValues_ReturnsGoodAndBadRows()
        {
            var input = new List<string>
            {
                "123x, 01/01/2022 11:00, 456",
                "234, 02/01/2022 12:00, 567"
            };
            using (var stream = GetMockFileStream(input))
            {
                var (good, bad) = _reader.GetRows(stream);

                bad.Count().Should().Be(1); 
                good.Count.Should().Be(1);

                bad.First().Should().Be(input.ElementAt(0));
                ReconstructString(good.First()).Should().Be(input.ElementAt(1));
            }
        }

        private MemoryStream GetMockFileStream(ICollection<string> fileRows)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream) { AutoFlush = true };
            foreach (var row in fileRows)
            {
                writer.WriteLine(row);
            }

            return stream;
        }

        private string ReconstructString(MeterReading reading)
        {
            var reconstructed = $"{reading.AccountId}, {reading.ReadingTime:dd/MM/yyyy HH:mm}, {reading.ReadingValue}";

            return reconstructed;
        }
    }
}
