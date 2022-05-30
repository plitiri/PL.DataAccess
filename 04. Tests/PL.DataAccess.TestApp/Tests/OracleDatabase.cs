using PL.DataAccess.TestApp.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PL.DataAccess.TestApp.Tests;

public class OracleDatabase
{
    public static async Task TestAsync(string connectionString)
    {
        var helper = DataAccessFactory.Create(DatabaseType.OracleDatabase, connectionString);
        if (helper != null)
        {
            var objectList1 = await helper.ExecuteListAsync("SELECT SYSDATE AS NOW FROM DUAL");
            Console.WriteLine(JsonSerializer.Serialize(objectList1, Constants.DefaultJsonSerializerOptions));

            var objectList2 = await helper.ExecuteListAsync<Sample01>("SELECT SYSDATE AS NOW FROM DUAL");
            Console.WriteLine(JsonSerializer.Serialize(objectList2, Constants.DefaultJsonSerializerOptions));
        }
    }
}
