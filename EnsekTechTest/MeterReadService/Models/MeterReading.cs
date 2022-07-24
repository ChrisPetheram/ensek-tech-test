using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadService.Models
{
    public class MeterReading
    {
        public int Id { get; set; }
        public int AccountId { get; init; }
        public DateTime ReadingTime { get; init; }
        public int ReadingValue { get; init; }
    }
}
