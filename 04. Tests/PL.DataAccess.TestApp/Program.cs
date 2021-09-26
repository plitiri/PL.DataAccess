using PL.DataAccess.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PL.DataAccess.TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // OracleDatabase
            {
                var helper = PL.DataAccess.Factory.CreateDataAccessHelper(DatabaseType.OracleDatabase, ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString);
                var objectList = await helper.ExecuteListAsync<Sample01>("SELECT SYSDATE AS NOW FROM DUAL");
                Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }

            // PostgreSQL
            {
                var helper = PL.DataAccess.Factory.CreateDataAccessHelper(DatabaseType.PostgreSQL, ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString);
                var objectList = await helper.ExecuteListAsync<Sample01>("select now() as now;");
                Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }
        }
    }

    public class Sample01
    {
        public DateTime? Now { get; set; }

        public override string ToString()
        {
            return $"Now: {Now}";
        }
    }
}
