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
                        catch(ArgumentException)
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
                                case TypeCode.Int32:
                                    property.SetValue(obj, isNullable ? ConvertHelper.ToNullableInt32(value) : ConvertHelper.ToInt32(value));
                                    break;
                                case TypeCode.Int64:
                                    property.SetValue(obj, isNullable ? ConvertHelper.ToNullableInt64(value) : ConvertHelper.ToInt64(value));
                                    break;
                                case TypeCode.DateTime:
                                    property.SetValue(obj, isNullable ? ConvertHelper.ToNullableDateTime(value) : ConvertHelper.ToDateTime(value));
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

    public static int? ToNullableInt32(object? obj)
    {
        try
        {
            return Convert.ToInt32(obj);
        }
        catch
        {
            return null;
        }
    }

    public static int ToInt32(object? obj)
    {
        try
        {
            return Convert.ToInt32(obj);
        }
        catch
        {
            return 0;
        }
    }

    public static long? ToNullableInt64(object? obj)
    {
        try
        {
            return Convert.ToInt64(obj);
        }
        catch
        {
            return null;
        }
    }

    public static long ToInt64(object? obj)
    {
        try
        {
            return Convert.ToInt64(obj);
        }
        catch
        {
            return 0;
        }
    }

    public static DateTime? ToNullableDateTime(object? obj)
    {
        try
        {
            return Convert.ToDateTime(obj);
        }
        catch
        {
            return null;
        }
    }

    public static DateTime ToDateTime(object? obj)
    {
        try
        {
            return Convert.ToDateTime(obj);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }
}
