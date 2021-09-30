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

            // MariaDB
            {
                var helper = PL.DataAccess.Factory.Create(DatabaseType.MariaDB, ConfigurationManager.ConnectionStrings["MariaDB"].ConnectionString);
                var objectList = await helper.ExecuteListAsync<Sample01>("select now() as now;");
                Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }

            // OracleDatabase
            {
                var helper = PL.DataAccess.Factory.Create(DatabaseType.OracleDatabase, ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString);
                var objectList = await helper.ExecuteListAsync<Sample01>("SELECT SYSDATE AS NOW FROM DUAL");
                Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }

            // PostgreSQL
            {
                var helper = PL.DataAccess.Factory.Create(DatabaseType.PostgreSQL, ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString);
                var objectList = await helper.ExecuteListAsync<Sample01>("select now() as now;");
                Console.WriteLine(JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }

            //{
            //    var helper = PL.DataAccess.Factory.CreateDataAccessHelper(DatabaseType.PostgreSQL, ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString);
            //    var objectList = await helper.ExecuteListAsync<MyTable>("select * from mytable where name = @name", new SqlParameterCollection() { { "NAME", "AA001" } });
            //}


            //{
            //    var helper = new PL.DataAccess.PostgreSQL.DatabaseAccessHelper(ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString);

            //    // List<List<ExpandoObject>> (means dataset)
            //    var response = await helper.ExecuteListsAsync("select * from mytable where name = @name", new SqlParameterCollection() { { "NAME", "AA001" } });

            //    // List<ExpandoObject> (means table)
            //    var expandoList = response.FirstOrDefault();
            //    var jsonString1 = JsonSerializer.Serialize(expandoList);
            //    Console.WriteLine(jsonString1);

            //    // List<MyTable> (means table)
            //    var objectList = ConvertHelper.ToGenericList<List<MyTable>>(expandoList);
            //    var jsonString2 = JsonSerializer.Serialize(objectList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            //    Console.WriteLine(jsonString2);

            //    // List<MyTable> (means table)
            //    var reversedObjectList = JsonSerializer.Deserialize<List<MyTable>>(jsonString2, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //    // List<List<MyTable>> (means dataset)
            //    var objectList2 = await helper.ExecuteListsAsync<MyTable>("select * from mytable where name = @name", new SqlParameterCollection() { { "NAME", "AA001" } });

            //    // List<MyTable> (means table)
            //    var objectList3 = await helper.ExecuteListAsync<MyTable>("select * from mytable where name = @name", new SqlParameterCollection() { { "NAME", "AA001" } });
            //}
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

    public class MyTable
    {
        public string Name { get; set; }
        public decimal Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
