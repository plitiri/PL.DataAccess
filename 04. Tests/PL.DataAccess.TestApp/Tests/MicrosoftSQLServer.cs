using PL.DataAccess.TestApp.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PL.DataAccess.TestApp.Tests;

public class MicrosoftSQLServer
{
    public static async Task TestAsync(string connectionString)
    {
        var helper = DataAccessFactory.Create(DatabaseType.MicrosoftSQLServer, connectionString);
        if (helper != null)
        {
            var objectList1 = await helper.ExecuteListAsync<Sample01>("select GETDATE() as now;");
            Console.WriteLine(JsonSerializer.Serialize(objectList1, DataAccessOptions.DefaultJsonSerializerOptions));

            var objectList2 = await helper.ExecuteListAsync("select GETDATE() as now;");
            Console.WriteLine(JsonSerializer.Serialize(objectList2, DataAccessOptions.DefaultJsonSerializerOptions));

            var objectList3 = await helper.ExecuteListAsync("select @name as name, @age as age, @now as now;", new() { { "NAME", "PL" }, { "AGE", 17.7 }, { "NOW", DateTime.Now } });
            Console.WriteLine(JsonSerializer.Serialize(objectList3, DataAccessOptions.DefaultJsonSerializerOptions));
        }
    }
}
