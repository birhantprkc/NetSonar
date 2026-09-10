/*
 *                     GNU AFFERO GENERAL PUBLIC LICENSE
 *                       Version 3, 19 November 2007
 *  Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 *  Everyone is permitted to copy and distribute verbatim copies
 *  of this license document, but changing it is not allowed.
 */

using System;
using Avalonia.Data.Converters;
using NetSonar.Avalonia.Extensions;

namespace NetSonar.Avalonia.Converters;

public class RawEnumToCollectionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is not Enum) return null;
        return EnumExtensions.GetAllValues(value.GetType());
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        var parameterString = parameter?.ToString();
        if (string.IsNullOrWhiteSpace(parameterString)) return null;
        return Enum.TryParse(targetType, parameterString, true, out var result)
            ? result
            : null;
    }
}