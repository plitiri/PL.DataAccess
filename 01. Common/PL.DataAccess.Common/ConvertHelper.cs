using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace PL.DataAccess;

public class ConvertHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expandoObjects"><see cref="System.Dynamic.ExpandoObject"/> list</param>
    /// <returns><typeparamref name="TValue"/></returns>
    public static TValue? ToGenericList<TValue>(IEnumerable<ExpandoObject> expandoObjects) where TValue : IList, new()
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

                        try
                        {
                            property.SetValue(obj, value == DBNull.Value ? null : value);
                        }
                        // Database Column 타입과 TValue Property 타입이 다를경우
                        catch(Exception ex)
                        {
                            var propertyType = property.PropertyType;
                            var isNullable = false;
                            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                isNullable = true;
                                propertyType = Nullable.GetUnderlyingType(property.PropertyType);
                            }

                            switch (Type.GetTypeCode(propertyType))
                            {
                                case TypeCode.Decimal:
                                    property.SetValue(obj, isNullable ? ConvertHelper.ToNullableDecimal(value) : ConvertHelper.ToDecimal(value));
                                    break;
                            }
                        }
                    }
                }
                response.Add(obj);
            }

            return response;
        }

        return default(TValue);
    }

    public static decimal? ToNullableDecimal(object? obj)
    {
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch
        {
            return null;
        }
    }

    public static decimal ToDecimal(object? obj)
    {
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch
        {
            return 0;
        }
    }
}
