using MeterReadService.Abstractions;

namespace MeterReadService.Models
{
    public class MeterReadUploadResponse : IUploadResponse
    {
        public string FileRow { get; init; }
        public MeterReadUploadState State { get; init; }
        public string StateName => State.ToString();
    }
}
