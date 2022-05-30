using System.Collections.Generic;

namespace PL.DataAccess;

/// <summary>
///  Same as Dictionary&lt;string, object&gt;
/// </summary>
public class CommandParameterCollection : Dictionary<string, object>
{
    /// <summary>
    /// Same as Dictionary&lt;string, object&gt;
    /// </summary>
    public CommandParameterCollection() : base() { }
}
