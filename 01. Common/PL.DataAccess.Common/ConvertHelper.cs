using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.DataAccess.Common
{
    public class ConvertHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expandoObjects"><see cref="System.Dynamic.ExpandoObject"/> list</param>
        /// <returns><typeparamref name="TValue"/></returns>
        public static TValue ToGenericList<TValue>(IEnumerable<ExpandoObject> expandoObjects) where TValue : IList, new()
        {
            var response = new TValue();

            var listType = typeof(TValue);
            var objectType = listType.GenericTypeArguments.FirstOrDefault();
            if (objectType != null)
            {
                var properties = objectType.GetProperties();
                foreach (var expandoObject in expandoObjects)
                {
                    var obj = Activator.CreateInstance(objectType);
                    foreach (var property in properties)
                    {
                        var query = expandoObject.Where(x => x.Key.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
                        if (query.Any())
                        {
                            var value = query.Select(x => x.Value).FirstOrDefault();
                            property.SetValue(obj, value);
                        }
                    }
                    response.Add(obj);
                }

                return response;
            }

            return default(TValue);
        }
    }
}
