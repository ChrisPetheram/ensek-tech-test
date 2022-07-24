using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadDatabaseAccess
{
    public interface IRepository<T>
    {
        bool Insert(T record);
        bool Exists(T record);
    }
}
