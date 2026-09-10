using Avalonia.Input;
using Microsoft.Win32;
using NetSonar.Avalonia.Extensions;
using StageKit;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using StageKit.Primitives.System;

namespace NetSonar.Avalonia.SystemOS;

public static class SystemAware
{
    /// <summary>
    /// Gets the key modifiers for the Control key or the Meta key on macOS.
    /// </summary>
    public static KeyModifiers ControlOrMeta => OperatingSystem.IsMacOS() ? KeyModifiers.Meta : KeyModifiers.Control;
    public static string ControlOrMetaString => OperatingSystem.IsMacOS() ? "Meta" : "Ctrl";
}