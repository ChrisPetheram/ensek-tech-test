﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadService.Models
{
    public class MeterReadUploadResponse
    {
        string FileRow { get; init; }
        MeterReadUploadState State { get; init; }
    }
}
