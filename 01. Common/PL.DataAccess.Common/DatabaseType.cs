using System;

namespace PL.DataAccess.Common
{
    public enum DatabaseType
    {
        /* RDBMS, Relational DataBase Management System
         */

        //MicrosoftSQLServer,
        MariaDB,
        //MySQL,
        OracleDatabase,
        PostgreSQL,
        //SQLite,

        /* NoSQL, non SQL
         */
        //MongoDB,

        /* TSDB, Time-series Databas
         */
        //InfluxDB,

    }
}
