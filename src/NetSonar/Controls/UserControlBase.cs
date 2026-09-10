using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using NetSonar.Avalonia.ViewModels;

namespace NetSonar.Avalonia.Controls;

public partial class UserControlBase : UserControl
{
    protected override Type StyleKeyOverride => typeof(UserControl);

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (DataContext is ViewModelBase model)
        {
            model.OnInitialized();
        }
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        if (DataContext is ViewModelBase model)
        {
            model.OnLoaded();
        }
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        if (DataContext is ViewModelBase model)
        {
            model.OnUnloaded();
        }
    }

    [RelayCommand]
    public static Task CopyToClipboard(object? obj)
    {
        return obj is null
            ? Task.CompletedTask
            : App.CopyToClipboard(obj.ToString());
    }
}