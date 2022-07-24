using MeterReadDatabaseAccess;
using MeterReadEntities;
using MeterReadService.Models;

namespace MeterReadService.Services
{
    public class MeterReadBulkUpload
    {
        private MeterReadingRepository repository;

        public MeterReadBulkUpload()
        {
            
        }

        public ICollection<MeterReadUploadResponse> ParseFile(Stream file)
        {
            var mapper = new MeterReadingCsvMapper();
            var reader = new CsvReader<MeterReading>(mapper);

            var responses = new List<MeterReadUploadResponse>();

            var (good, bad) = reader.GetRows(file);

            foreach (var row in good)
            {
                if (ShouldUploadRow(row.item))
                {
                    repository.InsertReading(row.item);
                }
            }



        }

        private bool ShouldUploadRow(MeterReading row)
        {
            throw new NotImplementedException();
        }
    }
}