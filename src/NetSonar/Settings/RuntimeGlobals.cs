using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NetSonar.Avalonia.Settings;

public partial class RuntimeGlobals : ObservableObject
{
    #region Properties

    public static TimeSpan ToastDismissAfter { get; } = TimeSpan.FromSeconds(6);

    /// <summary>
    /// Gets or sets the number of running threads.
    /// </summary>
    [ObservableProperty]
    public partial int RunningThreads { get; set; }


    #endregion
}