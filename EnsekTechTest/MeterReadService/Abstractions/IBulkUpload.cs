using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadService.Abstractions
{
    public interface IBulkUpload
    {
        public ICollection<IUploadResponse> ParseFile(Stream file);
    }
}
