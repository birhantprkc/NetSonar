using System;
using System.Drawing;

namespace NetSonar.Avalonia.Extensions;

public static class TypeExtensions
{
    public static bool IsSimpleType(this Type type)
    {
        return
            type.IsPrimitive 
            || type == typeof(string) 
            || type == typeof(decimal) 
            || type == typeof(Point) 
            || type == typeof(PointF) 
            || type == typeof(Size) 
            || type == typeof(SizeF) 
            || type == typeof(TimeOnly) 
            || type == typeof(DateTime) 
            || type == typeof(DateTimeOffset) 
            || type == typeof(TimeSpan) 
            || type == typeof(Guid) 
            || type.IsEnum 
            || IsNullableSimpleType(type)
            //|| Convert.GetTypeCode(type) != TypeCode.Object 
            //|| (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]))
            ;
    }

    public static bool IsNullableSimpleType(this Type t)
    {
        var underlyingType = Nullable.GetUnderlyingType(t);
        return underlyingType != null && IsSimpleType(underlyingType);
    }
}