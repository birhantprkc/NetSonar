using System;
using System.Drawing;
using Avalonia.Controls;
using Avalonia.Platform;

namespace NetSonar.Avalonia.Extensions;

public static class WindowExtensions
{
    public static Screen GetCurrentScreen(this Window window)
    {
        return //window.Screens.ScreenFromVisual(window) ??
            window.Screens.ScreenFromWindow(window) ??
            window.Screens.Primary ??
            window.Screens.All[0];
    }

    public static Size GetScreenWorkingArea(this Window window)
    {
        var screen = window.GetCurrentScreen();

        if (OperatingSystem.IsMacOS())
        {
            return new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
        }

        return new Size((int)(screen.WorkingArea.Width / screen.Scaling),(int)(screen.WorkingArea.Height / screen.Scaling));
    }
}