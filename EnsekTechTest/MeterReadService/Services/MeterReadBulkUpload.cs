﻿using MeterReadDatabaseAccess;
using MeterReadEntities;
using MeterReadService.Abstractions;
using MeterReadService.Models;

namespace MeterReadService.Services
{
    public class MeterReadBulkUpload : IBulkUpload
    {
        private IRepository<MeterReading> _repository;

        public MeterReadBulkUpload(IRepository<MeterReading> repository)
        {
            _repository = repository;
        }

        public ICollection<IUploadResponse> ParseFile(Stream file)
        {
            var mapper = new MeterReadingCsvMapper();
            var reader = new CsvReader<MeterReading>(mapper);

            var responses = new List<IUploadResponse>();

            var (good, bad) = reader.GetRows(file);
            PruneInvalidValues(good, bad);

            responses.AddRange(InsertGoodRows(good));
            responses.AddRange(HandleBadRows(bad));

            return responses;
        }

        private bool ShouldUploadRow(MeterReading row)
        {
            var exists = _repository.Exists(row);
            return !exists;
        }

        private void PruneInvalidValues(ICollection<(string row, MeterReading item)> successes, ICollection<string> failures)
        {
            for (int i = successes.Count - 1; i >= 0; i--)
            {
                if (!successes.ElementAt(i).item.IsValid())
                {
                    failures.Add(successes.ElementAt(i).row);
                    successes.Remove(successes.ElementAt(i));
                }
            }
        }

        private ICollection<MeterReadUploadResponse> InsertGoodRows(ICollection<(string row, MeterReading item)> good)
        {
            var responses = new List<MeterReadUploadResponse>();
            foreach (var row in good)
            {
                MeterReadUploadState state = MeterReadUploadState.AlreadyUploaded;
                if (ShouldUploadRow(row.item))
                {
                    var success = _repository.Insert(row.item);
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
            return responses;
        }

        private ICollection<MeterReadUploadResponse> HandleBadRows(ICollection<string> bad)
        {
            var responses = new List<MeterReadUploadResponse>();
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
    }
}