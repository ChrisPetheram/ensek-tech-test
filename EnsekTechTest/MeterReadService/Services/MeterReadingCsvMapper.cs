using MeterReadService.Abstractions;
using MeterReadService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
