using Avalonia.Controls;
using Avalonia.Interactivity;
using NetSonar.Avalonia.Controls;
using NetSonar.Avalonia.ViewModels.Dialogs;

namespace NetSonar.Avalonia.Views.Dialogs;

public partial class SetInterfaceIPDialogView : UserControlBase
{
    public SetInterfaceIPDialogView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        if (DataContext is SetInterfaceIPDialogModel model)
        {
            if (!model.NetworkInterface.SupportsIPv4)
            {
                if (model.NetworkInterface.SupportsIPv6) TabControlPanel.SelectedIndex = 1;
                else TabControlPanel.IsVisible = false;
            }

        }
        base.OnLoaded(e);
    }
}