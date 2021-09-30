using PL.DataAccess.Common;
using System;

namespace PL.DataAccess
{
    public class Factory
    {
        /// <summary>
        /// Create DatabaseAccessHelper
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <param name="databaseType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDatabaseAccessHelper Create(DatabaseType databaseType, string connectionString)
        {
            string typeName = string.Empty;
            switch (databaseType)
            {
                case DatabaseType.MariaDB:
                    typeName = "PL.DataAccess.Providers.MariaDB.DatabaseAccessHelper, PL.DataAccess.Providers.MariaDB";
                    break;
                case DatabaseType.OracleDatabase:
                    typeName = "PL.DataAccess.Providers.OracleDatabase.DatabaseAccessHelper, PL.DataAccess.Providers.OracleDatabase";
                    break;
                case DatabaseType.PostgreSQL:
                    typeName = "PL.DataAccess.Providers.PostgreSQL.DatabaseAccessHelper, PL.DataAccess.Providers.PostgreSQL";
                    break;
            }

            var type = Type.GetType(typeName);
            if(string.IsNullOrWhiteSpace(typeName))
            {
                throw new ExecuteException($"{databaseType} is not yet supported.");
            }
            else if(type == null)
            {
                throw new ExecuteException($"{typeName} Assembly reference is required.");
            }
            else
            {
                return Activator.CreateInstance(type, connectionString) as IDatabaseAccessHelper;
            }
        }
    }
}
