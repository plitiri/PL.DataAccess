# Database Access Helper
A wrapper library that makes it easy to access data from multiple relational databases using ado.net.

### Assembly
| PL.DataAccess

### Usage

1. Create Helper
    1. var helper = PL.DataAccess.Factory.CreateDataAccessHelper(**DatabaseType**, **ConnectionString**);
2. Execute
    1. var objectList = await helper.ExecuteListAsync\<TValue\>(**SQL**, **SqlParameterCollection**);
    2. var affectedCount = await helper.ExecuteNonQueryAsync\<TValue\>(**SQL**, **SqlParameterCollection**);

### Example

```csharp
public class Sample01
{
    public DateTime? Now { get; set; }
}
```

#### 1. MariaDB
```csharp
var helper = PL.DataAccess.Factory.CreateDataAccessHelper(DatabaseType.MariaDB, @"Server=localhost; Port=3306; User ID=root; Password=password; Database=mysql;");
var objectList = await helper.ExecuteListAsync<Sample01>("select now() as now;");
Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
```
| [{"now":"2021-01-01T00:00:00"}]

#### 2. OracleDatabase
```csharp
var helper = PL.DataAccess.Factory.CreateDataAccessHelper(DatabaseType.OracleDatabase, @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE))); User Id=C##ORAUSER; Password=ORAUSER;");
var objectList = await helper.ExecuteListAsync<Sample01>("SELECT SYSDATE AS NOW FROM DUAL");
Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
```
| [{"now":"2021-01-01T00:00:00"}]

#### 3. PostgreSQL
```csharp
var helper = PL.DataAccess.Factory.CreateDataAccessHelper(DatabaseType.PostgreSQL, "User ID=postgres; Password=postgres; Host=localhost; Port=5432; Database=postgres; Pooling=true; Connection Lifetime=0;");
var objectList = await helper.ExecuteListAsync<Sample01>("select now() as now;");
Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
```
| [{"now":"2021-01-01T00:00:00.000000+09:00"}]

### Reference
* [MySqlConnector](https://www.nuget.org/packages/MySqlConnector/)
* [Oracle.ManagedDataAccess.Core](https://www.nuget.org/packages/Oracle.ManagedDataAccess.Core/)
* [Npgsql](https://www.nuget.org/packages/Npgsql/)
