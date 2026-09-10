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
using ZLinq;

namespace NetSonar.Avalonia.Converters;

public class FromValueDescriptionToEnumConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null) return null;
        var list = EnumExtensions.GetAllValuesAndDescriptions(value.GetType());
        return list
            .AsValueEnumerable()
            .FirstOrDefault(vd => Equals(vd.Value, value));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is null) return null;
        var list = EnumExtensions.GetAllValuesAndDescriptions(targetType);
        return list
            .AsValueEnumerable()
            .FirstOrDefault(vd => vd.Description == value.ToString())?.Value;
    }
}