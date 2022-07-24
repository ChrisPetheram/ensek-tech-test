using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadService.Models
{
    public enum MeterReadUploadState
    {
        UploadSuccessful,
        AlreadyUploaded,
        CouldNotParse
    }
}
