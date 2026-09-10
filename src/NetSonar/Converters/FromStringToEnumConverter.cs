/*
 *                     GNU AFFERO GENERAL PUBLIC LICENSE
 *                       Version 3, 19 November 2007
 *  Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 *  Everyone is permitted to copy and distribute verbatim copies
 *  of this license document, but changing it is not allowed.
 */

using System;
using System.Linq;
using Avalonia.Data.Converters;
using ZLinq;

namespace NetSonar.Avalonia.Converters;

public class FromStringToEnumConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null) return null;
        var list = Enum.GetValues(value.GetType()).Cast<Enum>().AsValueEnumerable();
        return list.FirstOrDefault(vd => Equals(vd, value));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is null) return null;
        var list = Enum.GetValues(value.GetType()).Cast<Enum>().AsValueEnumerable();
        return list.FirstOrDefault(vd => Equals(vd, value));
    }
}