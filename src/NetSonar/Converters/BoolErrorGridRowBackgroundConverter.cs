using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;
using System;

namespace NetSonar.Avalonia.Converters;

public class BoolErrorGridRowBackgroundConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool success) return null;
        return success ? null : Brushes.DarkRed;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}