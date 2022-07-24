using Dapper;
using MeterReadEntities;
using System.Data;

namespace MeterReadDatabaseAccess
{
    public class MeterReadingRepository : IRepository<MeterReading>
    {
        private IDbConnector _connector;

        public MeterReadingRepository(IDbConnector connector)
        {
            _connector = connector;
        }

        public bool Insert(MeterReading reading)
        {
            using (var connection = _connector.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("AccountId", reading.AccountId);
                parameters.Add("ReadingTime", reading.ReadingTime);
                parameters.Add("ReadingValue", reading.ReadingValue);

                var itemId = connection.QueryFirstOrDefault<int>(
                    new CommandDefinition(
                        "[dbo].[InsertMeterReading]",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        ));

                if (itemId == 0)
                {
                    return false;
                }

                reading.Id = itemId;
                return true;
            }
        }

        public bool Exists(MeterReading reading)
        {
            using (var connection = _connector.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("AccountId", reading.AccountId);
                parameters.Add("ReadingTime", reading.ReadingTime);
                parameters.Add("ReadingValue", reading.ReadingValue);

                var exists = connection.QueryFirstOrDefault<int>(
                    new CommandDefinition(
                        "[dbo].[MeterReadingExists]",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        ));

                return (exists) > 0;
            }
            
        }
    }
}