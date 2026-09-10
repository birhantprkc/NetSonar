using System;
using System.ComponentModel;

namespace NetSonar.Avalonia;

public enum ApplicationTheme
{
    [Description("Default from system")]
    Default,

    [Description("Light")]
    Light,
    [Description("Dark")]
    Dark,
}

[Flags]
public enum MessageBoxButtons
{
    Yes = 0,
    No = 1,
    Cancel = 2,
}

public enum ErrorCodes
{
    Unknown = -1,
    Success = 0,
    Cancelled,
}

public enum CollectionSide
{
    Head,
    Tail
}