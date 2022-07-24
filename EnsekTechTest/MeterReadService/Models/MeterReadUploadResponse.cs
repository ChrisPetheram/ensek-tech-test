using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadService.Models
{
    public class MeterReadUploadResponse
    {
        public string FileRow { get; init; }
        public MeterReadUploadState State { get; init; }
        public string StateName => State.ToString();
    }
}
