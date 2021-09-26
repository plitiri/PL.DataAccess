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
        public string ConnectionString { get; }
        public CommandType CommandType { get; set; }
        public Task<int> ExecuteNonQueryAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);
        public Task<IList<IList<ExpandoObject>>> ExecuteListsAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);
        public Task<IList<IList<TValue>>> ExecuteListsAsync<TValue>(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);
        public Task<IList<ExpandoObject>> ExecuteListAsync(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);
        public Task<IList<TValue>> ExecuteListAsync<TValue>(string commandText, IDictionary<string, object> parameters = default, CancellationToken cancellationToken = default);
    }
}
