using PL.DataAccess.TestApp.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PL.DataAccess.TestApp.Tests;

public class PostgreSQL
{
    public static async Task TestAsync(string connectionString)
    {
        var helper = DataAccessFactory.Create(DatabaseType.PostgreSQL, connectionString);
        if (helper != null)
        {
            var objectList1 = await helper.ExecuteListAsync("select now() as now;");
            Console.WriteLine(JsonSerializer.Serialize(objectList1, DataAccessOptions.DefaultJsonSerializerOptions));

            var objectList2 = await helper.ExecuteListAsync<Sample01>("select now() as now;");
            Console.WriteLine(JsonSerializer.Serialize(objectList2, DataAccessOptions.DefaultJsonSerializerOptions));

            var objectList3 = await helper.ExecuteListAsync<MyTable>("select * from mytable where name = @name", new() { { "NAME", "AA001" } });
            Console.WriteLine(JsonSerializer.Serialize(objectList3, DataAccessOptions.DefaultJsonSerializerOptions));
        }
    }
}
