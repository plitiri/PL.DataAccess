using System;

namespace PL.DataAccess;

public class DataAccessFactory
{
    /// <summary>
    /// Create DatabaseAccessHelper
    /// </summary>
    /// <exception cref="ExecuteException"></exception>
    /// <param name="databaseType"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IDataAccessHelper? Create(DatabaseType databaseType, string connectionString)
    {
        string typeName = string.Empty;
        switch (databaseType)
        {
            case DatabaseType.MicrosoftSQLServer:
            case DatabaseType.MariaDB:
            case DatabaseType.OracleDatabase:
            case DatabaseType.PostgreSQL:
                typeName = $"PL.DataAccess.Providers.{databaseType}.DataAccessHelper, PL.DataAccess.Providers.{databaseType}";
                break;
        }

        var type = Type.GetType(typeName);
        if(string.IsNullOrWhiteSpace(typeName))
        {
            //throw new ExecuteException($"{databaseType} is not yet supported.");
            return null;
        }
        else if(type == null)
        {
            //throw new ExecuteException($"{typeName} Assembly reference is required.");
            return null;
        }
        else
        {
            return Activator.CreateInstance(type, connectionString) as IDataAccessHelper;
        }
    }
}
