using MeterReadService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadService.Services
{
    public class CsvReader<T>
    {
        public ICollection<T> GetRows(ICsvMapper<T> csvMapper)
        {
            throw new NotImplementedException();
        }
    }
}
