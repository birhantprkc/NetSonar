using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetSonar.Avalonia.Extensions;
using ZLinq;

namespace NetSonar.Avalonia.Cache;

public sealed class PropertiesSnapshot
{
    private readonly Dictionary<string, object?> _propertyCache = [];

    private object? _rootObject;

    /// <summary>
    /// Takes a snapshot of the properties of the object.
    /// </summary>
    /// <param name="obj"></param>
    public void Snapshot(object obj)
    {
        _rootObject = obj;
        _propertyCache.Clear();
        var properties = ReflectionExtensions.GetProperties(obj);
        foreach (var property in properties)
        {
            if (!property.CanWrite) continue;
            if (property.GetSetMethod(false) is null) continue;
            var value = property.GetValue(obj);
            _propertyCache.Add(property.Name, value);
        }
    }

    /// <summary>
    /// Determines if the property has changed.
    /// </summary>
    /// <param name="currentValue"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public bool IsChanged(object? currentValue, [CallerArgumentExpression(nameof(currentValue))] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);
        if(_propertyCache.TryGetValue(propertyName, out var value))
        {
            return !Equals(value, currentValue);
        }

        return false;
    }

    /// <summary>
    /// Determines if any of the properties have changed.
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    public bool IsChangedAnyOf(IEnumerable<KeyValuePair<string, object?>> properties)
    {
        return properties
            .AsValueEnumerable()
            .Any(property => IsChanged(property.Value, property.Key));
    }
}