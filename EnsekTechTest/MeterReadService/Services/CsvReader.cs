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
        private ICsvMapper<T> _mapper;

        public CsvReader(ICsvMapper<T> mapper)
        {
            _mapper = mapper;
        }

        public (ICollection<T> successes, ICollection<string> failures) GetRows(Stream file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (_mapper == null)
                throw new ArgumentNullException(nameof(_mapper));

            var successes = new List<T>();
            var failures = new List<string>();
            string currentLine = null;

            using (var reader = new StreamReader(file))
            {
                file.Seek(0, SeekOrigin.Begin);

                while (reader.Peek() >= 0)
                {
                    try
                    {
                        currentLine = reader.ReadLine();
                        var values = currentLine?.Split(",", StringSplitOptions.TrimEntries);

                        successes.Add(_mapper.ColumnMapping(values!));

                    }
                    catch (FormatException fEx)
                    {
                        failures.Add(currentLine!);
                        continue;
                    }
                }
            }
            
            return (successes, failures);
        }
    }
}
