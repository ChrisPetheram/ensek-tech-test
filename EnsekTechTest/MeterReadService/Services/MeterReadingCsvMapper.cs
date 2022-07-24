using MeterReadEntities;
using MeterReadService.Abstractions;
using System.Globalization;

namespace MeterReadService.Services
{
    public class MeterReadingCsvMapper : ICsvMapper<MeterReading>
    {
        public Func<ICollection<string>, MeterReading> ColumnMapping =>
            columns => new MeterReading
            {
                AccountId = int.Parse(columns.ElementAt(0)),
                ReadingTime = DateTime.ParseExact(columns.ElementAt(1), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                ReadingValue = int.Parse(columns.ElementAt(2))
            };
    }
}
