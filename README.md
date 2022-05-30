# Database Access Helper
A wrapper library that makes it easy to access data from multiple relational databases using ado.net.

## Assembly
| PL.DataAccess

## Usage

1. Create Helper
    * var helper = DataAccessFactory.Create(**DatabaseType**, **ConnectionString**);
2. Execute
    1. SELECT
       * var objectList = await helper.ExecuteListAsync(**SQL**, **CommandParameterCollection**);
       * var objectList = await helper.ExecuteListAsync\<TValue\>(**SQL**, **CommandParameterCollection**);
    2. INSERT, UPDATE, DELETE
       * var affectedCount = await helper.ExecuteNonQueryAsync(**SQL**, **CommandParameterCollection**);

## Example

### 1. MicrosoftSQLServer
```csharp
var helper = DataAccessFactory.Create(DatabaseType.MicrosoftSQLServer, @"Database Source=localhost,1433; User ID=sa; Password=password; Database=sql;");
var objectList = await helper.ExecuteListAsync("SELECT @name AS name, GETDATE() as now;", new() { { "NAME", "PL" } });
Console.WriteLine(JsonSerializer.Serialize(objectList, DataAccessOptions.DefaultJsonSerializerOptions));
```
| [{"name":"PL", "now":"2021-01-01T00:00:00"}]

### 2. MariaDB
```csharp
var helper = DataAccessFactory.Create(DatabaseType.MariaDB, @"Server=localhost; Port=3306; User ID=root; Password=password; Database=mysql;");
var objectList = await helper.ExecuteListAsync("SELECT @name AS name, NOW() as now;", new() { { "NAME", "PL" } });
Console.WriteLine(JsonSerializer.Serialize(objectList, DataAccessOptions.DefaultJsonSerializerOptions));
```
| [{"name":"PL", "now":"2021-01-01T00:00:00"}]

### 3. OracleDatabase
```csharp
var helper = DataAccessFactory.Create(DatabaseType.OracleDatabase, @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE))); User Id=C##ORAUSER; Password=ORAUSER;");
var objectList = await helper.ExecuteListAsync("SELECT :name AS name, SYSDATE AS NOW FROM DUAL", new() { { "NAME", "PL" } });
Console.WriteLine(JsonSerializer.Serialize(objectList, DataAccessOptions.DefaultJsonSerializerOptions));
```
| [{"name":"PL", "now":"2021-01-01T00:00:00"}]

### 4. PostgreSQL
```csharp
var helper = DataAccessFactory.Create(DatabaseType.PostgreSQL, "Host=localhost; Port=5432; User ID=postgres; Password=postgres; Database=postgres; Pooling=true; Connection Lifetime=0;");
var objectList = await helper.ExecuteListAsync("SELECT @name AS name, NOW() as now;", new() { { "NAME", "PL" } });
Console.WriteLine(JsonSerializer.Serialize(objectList, DataAccessOptions.DefaultJsonSerializerOptions));
```
| [{"name":"PL", "now":"2021-01-01T00:00:00.000000+09:00"}]

## Dependency
* [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient)
* [MySqlConnector](https://www.nuget.org/packages/MySqlConnector/)
* [Oracle.ManagedDataAccess.Core](https://www.nuget.org/packages/Oracle.ManagedDataAccess.Core/)
* [Npgsql](https://www.nuget.org/packages/Npgsql/)
