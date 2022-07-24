using MeterReadDatabaseAccess;
using MeterReadEntities;
using MeterReadService.Models;

namespace MeterReadService.Services
{
    public class MeterReadBulkUpload
    {
        private MeterReadingRepository _repository;

        public MeterReadBulkUpload(MeterReadingRepository repository)
        {
            _repository = repository;
        }

        public ICollection<MeterReadUploadResponse> ParseFile(Stream file)
        {
            var mapper = new MeterReadingCsvMapper();
            var reader = new CsvReader<MeterReading>(mapper);

            var responses = new List<MeterReadUploadResponse>();

            var (good, bad) = reader.GetRows(file);

            foreach (var row in good)
            {
                MeterReadUploadState state = MeterReadUploadState.AlreadyUploaded;
                if (ShouldUploadRow(row.item))
                {
                    var success = _repository.InsertReading(row.item);
                    state = success
                        ? MeterReadUploadState.UploadSuccessful
                        : MeterReadUploadState.CouldNotUpload;
                }

                responses.Add(new MeterReadUploadResponse
                {
                    FileRow = row.row,
                    State = state
                });
            }

            foreach (var row in bad)
            {
                responses.Add(new MeterReadUploadResponse
                {
                    FileRow = row,
                    State = MeterReadUploadState.CouldNotParse
                });
            }

            return responses;
        }

        private bool ShouldUploadRow(MeterReading row)
        {
            var exists = _repository.EntryExists(row);
            return !exists;
        }
    }
}