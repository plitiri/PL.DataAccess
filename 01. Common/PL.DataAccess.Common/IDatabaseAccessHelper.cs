using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PL.DataAccess.Common
{
    public interface IDatabaseAccessHelper
    {
        public DatabaseType DatabaseType { get; }

        /// <summary>
        /// "User ID=postgres; Password=postgres; Host=localhost; Port=5432; Database=postgres; Pooling=true; Connection Lifetime=0;"
        /// </summary>
        public string ConnectionString { get; }

        public CommandType CommandType { get; set; }

        public Task<int> ExecuteNonQueryAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Property names are not case-sensitive.
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <param name="commandText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list with an <see cref="System.Dynamic.ExpandoObject"/> list</returns>
        public Task<IList<IList<ExpandoObject>>> ExecuteListsAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list with an <typeparamref name="TValue"/> list</returns>
        public Task<IList<IList<TValue>>> ExecuteListsAsync<TValue>(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="System.Dynamic.ExpandoObject"/> list</returns>
        public Task<IList<ExpandoObject>> ExecuteListAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ExecuteException"></exception>
        /// <typeparam name="TValue">asdf</typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><typeparamref name="TValue"/> list</returns>
        public Task<IList<TValue>> ExecuteListAsync<TValue>(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);
    }
}
