/*
 *                     GNU AFFERO GENERAL PUBLIC LICENSE
 *                       Version 3, 19 November 2007
 *  Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 *  Everyone is permitted to copy and distribute verbatim copies
 *  of this license document, but changing it is not allowed.
 */

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NetSonar.Avalonia.Models;

public record DescriptionValue<T>
{
    public string Description { get; init; } = string.Empty;
    public required T Value { get; init; }


    public DescriptionValue()
    {
    }

    [SetsRequiredMembers]
    public DescriptionValue(string description, T value)
    {
        Description = description;
        Value = value;
    }
}


public partial class ValueDescription : ObservableObject, IEquatable<ValueDescription>
{
    [ObservableProperty]
    public partial object? Value { get; set; }
    [ObservableProperty]
    public partial string? Description { get; set; }
    [XmlIgnore]
    [JsonIgnore]
    public string ValueAsString
    {
        get => Value?.ToString() ?? string.Empty;
        set => Value = value;
    }

    public ValueDescription()
    {
    }

    public ValueDescription(object value, string? description = null)
    {
        Value = value;
        Description = description;
    }
    public ValueDescription(object value, object? description = null)
    {
        Value = value;
        Description = description?.ToString();
    }

    public bool Equals(ValueDescription? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Value, other.Value) && Description == other.Description;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ValueDescription) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Description);
    }

    public override string? ToString()
    {
        return Description;
    }
}