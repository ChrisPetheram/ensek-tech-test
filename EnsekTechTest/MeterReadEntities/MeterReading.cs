using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadEntities
{
    public class MeterReading
    {
        private int _meterDigits = 5;

        public int Id { get; set; }
        public int AccountId { get; init; }
        public DateTime ReadingTime { get; init; }
        public int ReadingValue { get; init; }

        public bool IsValid()
        {
            if (ReadingValue < 0) return false;

            var maxValue = Math.Pow(10, _meterDigits) - 1;
            if (ReadingValue > maxValue) return false;

            return true;
        }
    }
}
