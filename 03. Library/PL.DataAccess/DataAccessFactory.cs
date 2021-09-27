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
                case DatabaseType.MariaDB:
                    return new Providers.MariaDB.DatabaseAccessHelper(connectionString);
                case DatabaseType.OracleDatabase:
                    return new Providers.OracleDatabase.DatabaseAccessHelper(connectionString);
                case DatabaseType.PostgreSQL:
                    return new Providers.PostgreSQL.DatabaseAccessHelper(connectionString);
                default:
                    return null;
            }
        }
    }
}
