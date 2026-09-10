using System;
using SukiUI.Controls;

namespace NetSonar.Avalonia.Controls;

public class SukiWindowExtended : SukiWindow
{
    protected override Type StyleKeyOverride => typeof(SukiWindow);

    protected override void OnClosed(EventArgs e)
    {
        if (DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }

        base.OnClosed(e);
    }
}