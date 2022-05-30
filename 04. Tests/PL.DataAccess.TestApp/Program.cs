using PL.DataAccess;
using PL.DataAccess.TestApp.Models;
using PL.DataAccess.TestApp.Tests;
using System;
using System.Configuration;
using System.Text.Json;

Console.WriteLine("* DataAccessHelper");
Console.WriteLine("  1. Change Database ConnectionString in App.config");
Console.WriteLine("  2. Uncomment and test");
Console.WriteLine(" --------------------------------------------------------");

// MicrosoftSQLServer
//await MicrosoftSQLServer.TestAsync(ConfigurationManager.ConnectionStrings["MicrosoftSQLServer"].ConnectionString);

// MariaDB
//await MariaDB.TestAsync(ConfigurationManager.ConnectionStrings["MariaDB"].ConnectionString);

// OracleDatabase
//await OracleDatabase.TestAsync(ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString);

// PostgreSQL
//await PostgreSQL.TestAsync(ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString);
