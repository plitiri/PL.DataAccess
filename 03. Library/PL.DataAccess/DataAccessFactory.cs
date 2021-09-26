using PL.DataAccess.Common;
using System;

namespace PL.DataAccess
{
    public class Factory
    {
        public static IDatabaseAccessHelper CreateDataAccessHelper(DatabaseType databaseType, string connectionString)
        {
            switch(databaseType)
            {
                case DatabaseType.PostgreSQL: 
                    return new PostgreSQL.DatabaseAccessHelper(connectionString);
                case DatabaseType.OracleDatabase:
                    return new OracleDatabase.DatabaseAccessHelper(connectionString);
                default:
                    return null;
            }
        }
    }
}
