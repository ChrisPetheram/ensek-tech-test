﻿using MeterReadService.Abstractions;

namespace MeterReadService.Services
{
    public class CsvReader<T>
    {
        private ICsvMapper<T> _mapper;

        public CsvReader(ICsvMapper<T> mapper)
        {
            _mapper = mapper;
        }

        public (ICollection<(string row, T item)> successes, ICollection<string> failures) GetRows(Stream file, bool hasHeaders = true)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (_mapper == null)
                throw new ArgumentNullException(nameof(_mapper));

            var successes = new List<(string, T)>();
            var failures = new List<string>();
            string currentLine = null;

            file.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(file))
            {
                if (hasHeaders && reader.Peek() >= 0)
                {
                    _ = reader.ReadLine();
                }

                while (reader.Peek() >= 0)
                {
                    try
                    {
                        currentLine = reader.ReadLine();
                        var values = currentLine?.Split(",", StringSplitOptions.TrimEntries);

                        successes.Add((currentLine!, _mapper.ColumnMapping(values!)));

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
