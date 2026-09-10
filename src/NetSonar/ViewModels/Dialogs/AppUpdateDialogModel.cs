using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Octokit;
using SukiUI.Dialogs;
using StageKit.Updatum;

namespace NetSonar.Avalonia.ViewModels.Dialogs;

public partial class AppUpdateDialogModel : DialogViewModelBase
{
    public static UpdatumManager AppUpdater => App.AppUpdater;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public Release Release { get; }

    public static string? Changelog => AppUpdater.GetChangelog();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AutoUpdateCommand))]
    [NotifyPropertyChangedFor(nameof(IsIdle))]
    public partial bool IsDownloading { get; set; }

    public bool IsIdle => !IsDownloading;

    public AppUpdateDialogModel()
    {
        Release = new Release();
    }

    public AppUpdateDialogModel(ISukiDialog dialog, Release release) : base(dialog)
    {
        Release = release;
    }


    [RelayCommand(CanExecute = nameof(IsIdle))]
    public async Task AutoUpdate()
    {
        IsDownloading = true;
        try
        {
            var download = await AppUpdater.DownloadAndInstallUpdateAsync(Release, _cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            // ignored
        }
        catch (Exception e)
        {
            App.ShowExceptionToast(e);
        }

        IsDownloading = false;
        Dialog.Dismiss();
    }

    [RelayCommand]
    public void ManualUpdate()
    {
        LaunchUriAsync(Release.Url);
        Dialog.Dismiss();
    }

    [RelayCommand]
    public void CancelUpdate()
    {
        _cancellationTokenSource.Cancel();
    }

    protected internal override void OnUnloaded()
    {
        _cancellationTokenSource.Dispose();
    }
}