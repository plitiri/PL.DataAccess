using Oracle.ManagedDataAccess.Client;
using PL.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;

namespace PL.DataAccess.Providers.OracleDatabase
{
    /// <summary>
    /// Assistant
    /// </summary>
    public class DatabaseAccessHelper : IDatabaseAccessHelper
    {
        public DatabaseType DatabaseType => DatabaseType.OracleDatabase;

        /// <summary>
        /// "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE))); User Id=C##ORAUSER; Password=ORAUSER;"
        /// </summary>
        public string ConnectionString => ConnectionStringBuilder?.ToString();

        public CommandType CommandType { get; set; } = CommandType.Text;

        private readonly OracleConnectionStringBuilder ConnectionStringBuilder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE))); User Id=C##ORAUSER; Password=ORAUSER;"</param>
        public DatabaseAccessHelper(string connectionString)
        {
            ConnectionStringBuilder = new OracleConnectionStringBuilder(connectionString);
        }

        public DatabaseAccessHelper(OracleConnectionStringBuilder connectionStringBuilder)
        {
            ConnectionStringBuilder = connectionStringBuilder;
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default)
        {
            using (var connection = new OracleConnection(ConnectionStringBuilder.ToString()))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandType = this.CommandType;
                    command.CommandText = commandText;
                    if (parameters != null)
                    {
                        foreach (var keyValuePair in parameters)
                        {
                            command.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }

                    return await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }

        /// <summary>
        /// Property names are not case-sensitive.
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <param name="commandText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list with an <see cref="System.Dynamic.ExpandoObject"/> list</returns>
        public async Task<IList<IList<ExpandoObject>>> ExecuteListsAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default)
        {
            var response = new List<IList<ExpandoObject>>();
            using (var connection = new OracleConnection(ConnectionStringBuilder.ToString()))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandType = this.CommandType;
                    command.CommandText = commandText;
                    if (parameters != null)
                    {
                        foreach (var keyValuePair in parameters)
                        {
                            command.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                    using (var reader = await command.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken))
                    {
                        do
                        {
                            var list = new List<ExpandoObject>();
                            while (await reader.ReadAsync())
                            {
                                var obj = new ExpandoObject();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var name = reader.GetName(i);
                                    var type = reader.GetFieldType(i);
                                    var value = reader.GetValue(i);

                                    if (obj.TryAdd(name, value) == false)
                                    {
                                        throw new ExecuteException("Adding ExpandObject to List<ExpandoObject> failed.");
                                    }
                                }
                                list.Add(obj);
                            }
                            response.Add(list);
                        } while (await reader.NextResultAsync());
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list with an <typeparamref name="TValue"/> list</returns>
        public async Task<IList<IList<TValue>>> ExecuteListsAsync<TValue>(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default)
        {
            var response = new List<IList<TValue>>();

            foreach (var list in await ExecuteListsAsync(commandText, parameters, cancellationToken))
            {
                response.Add(ConvertHelper.ToGenericList<List<TValue>>(list));
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="System.Dynamic.ExpandoObject"/> list</returns>
        public async Task<IList<ExpandoObject>> ExecuteListAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default)
        {
            var response = new List<ExpandoObject>();
            using (var connection = new OracleConnection(ConnectionStringBuilder.ToString()))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandType = this.CommandType;
                    command.CommandText = commandText;
                    if (parameters != null)
                    {
                        foreach (var keyValuePair in parameters)
                        {
                            command.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                    using (var reader = await command.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken))
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new ExpandoObject();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i);
                                var type = reader.GetFieldType(i);
                                var value = reader.GetValue(i);

                                if (obj.TryAdd(name, value) == false)
                                {
                                    throw new ExecuteException("Adding ExpandObject to List<ExpandoObject> failed.");
                                }
                            }
                            response.Add(obj);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <typeparam name="TValue">asdf</typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><typeparamref name="TValue"/> list</returns>
        public async Task<IList<TValue>> ExecuteListAsync<TValue>(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default)
        {
            return ConvertHelper.ToGenericList<List<TValue>>(await ExecuteListAsync(commandText, parameters, cancellationToken));
        }
    }
}
