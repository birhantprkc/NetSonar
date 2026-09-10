using System;
using Avalonia.Data.Converters;
using NetSonar.Avalonia.Extensions;

namespace NetSonar.Avalonia.Converters;

public class FileSizeNormalizeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null) return null;
        if (value is sbyte or byte or short or ushort or int or uint or long or ulong or float or double or decimal)
        {
            return ConverterExtension.ToFileSizeString((long)value);
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        return null;
    }
}