using System;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NetSonar.Avalonia.Models;

public partial class GroupNameValue : ObservableObject
{
    [ObservableProperty]
    public partial string Group { get; set; } = string.Empty;

    [ObservableProperty]
    public required partial string Name { get; set; } = string.Empty;

    public string Value
    {
        get;
        set
        {
            if (value.Equals("True")) value = "\u2705";
            else if (value.Equals("False")) value = "\u274c";
            SetProperty(ref field, value);
        }
    } = string.Empty;

    public GroupNameValue()
    {
    }

    [SetsRequiredMembers]
    public GroupNameValue(string name, string? value = "", string group = "")
    {
        Name = name;
        Value = value ?? string.Empty;
        Group = group;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}, {nameof(Group)}: {Group}";
    }
}