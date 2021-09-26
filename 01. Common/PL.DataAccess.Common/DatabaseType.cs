using System;

namespace PL.DataAccess.Common
{
    public enum DatabaseType
    {
        /* RDBMS, Relational DataBase Management System
         */
        OracleDatabase,
        //MicrosoftSQLServer,
        PostgreSQL,
        //MariaDB,
        //MySQL,
        //SQLite,

        /* NoSQL, non SQL
         */
        //MongoDB,

        /* TSDB, Time-series Databas
         */
        //InfluxDB,

    }
}
