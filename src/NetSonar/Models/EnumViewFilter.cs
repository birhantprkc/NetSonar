using System;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using Material.Icons.Avalonia;
using NetSonar.Avalonia.Extensions;
using StageKit.Primitives.Extensions;

namespace NetSonar.Avalonia.Models;

public partial class EnumViewFilter : ObservableObject
{
    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool Include { get; set; } = true;

    [JsonIgnore]
    public MaterialIconKind IconKind { get; set; } = MaterialIconKind.SetNone;

    [JsonIgnore]
    public MaterialIcon Icon => new(){ Kind = IconKind };

    public EnumViewFilter()
    {
    }

    public EnumViewFilter(Enum enumValue, bool value = true)
    {
        Description = enumValue.ToString().InsertCharBetweenCamelCase();
        Include = value;
    }

    public EnumViewFilter(string description, bool value = true)
    {
        Description = description;
        Include = value;
    }
}