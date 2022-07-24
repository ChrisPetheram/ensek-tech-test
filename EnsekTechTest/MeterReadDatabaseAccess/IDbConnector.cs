using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadDatabaseAccess
{
    public interface IDbConnector
    {
        string ConnectionString { get; init; }

        public IDbConnection GetConnection();
    }
}
