using Avalonia.Input;
using NetSonar.Avalonia.Controls;
using NetSonar.Avalonia.Settings;
using NetSonar.Avalonia.SystemOS;

namespace NetSonar.Avalonia.Views;

public partial class GenericWindow : SukiWindowExtended
{
    public GenericWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    public static AppSettings AppSettings => AppSettings.Instance;

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.Key is Key.OemPlus or Key.Add && e.KeyModifiers == (SystemAware.ControlOrMeta | KeyModifiers.Alt))
        {
            AppSettings.UiScale += 0.05f;
            e.Handled = true;
        }
        else if (e.Key is Key.OemMinus or Key.Subtract &&
                 e.KeyModifiers == (SystemAware.ControlOrMeta | KeyModifiers.Alt))
        {
            AppSettings.UiScale -= 0.05f;
            e.Handled = true;
        }
        else if (e.Key is Key.D0 or Key.NumPad0 && e.KeyModifiers == (SystemAware.ControlOrMeta | KeyModifiers.Alt))
        {
            AppSettings.UiScale = 1.0f;
            e.Handled = true;
        }

        base.OnKeyUp(e);
    }
}