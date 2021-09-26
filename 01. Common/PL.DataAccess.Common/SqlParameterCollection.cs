using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.DataAccess.Common
{
    /// <summary>
    ///  Same as Dictionary&lt;string, object&gt;
    /// </summary>
    public class SqlParameterCollection : Dictionary<string, object>
    {
        /// <summary>
        /// Same as Dictionary&lt;string, object&gt;
        /// </summary>
        public SqlParameterCollection() : base() { }
    }
}
