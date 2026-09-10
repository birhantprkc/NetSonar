using CommunityToolkit.Mvvm.Input;
using System;
using Avalonia.Controls;

namespace NetSonar.Avalonia.ViewModels.Dialogs;

public partial class InstanceAlreadyRunningDialogModel : ViewModelBase
{
    public string Message { get; init; }

    public InstanceAlreadyRunningDialogModel() : this(Design.IsDesignMode ? 1001 : null)
    {
    }

    public InstanceAlreadyRunningDialogModel(int? primaryProcessId)
    {
        Message = App.Localization.Format("InstanceAlreadyRunning.Message", App.Software);

        if (primaryProcessId is not null)
        {
            Message += App.Localization.Format("Common.ProcessId", primaryProcessId);
        }
    }

    [RelayCommand]
    public static void CloseWindow()
    {
        Environment.Exit(0);
    }
}
