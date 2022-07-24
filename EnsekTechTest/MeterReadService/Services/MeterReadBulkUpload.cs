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

            var (good, bad) = reader.GetRows(file);



            throw new NotImplementedException();
        }
    }
}