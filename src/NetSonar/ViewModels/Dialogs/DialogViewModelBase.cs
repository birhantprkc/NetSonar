using CommunityToolkit.Mvvm.Input;
using SukiUI.Controls;
using SukiUI.Dialogs;
using System.Threading.Tasks;

namespace NetSonar.Avalonia.ViewModels.Dialogs;

public partial class DialogViewModelBase : ViewModelBase
{
    protected readonly ISukiDialog Dialog;

    /// <summary>
    /// Gets or sets a value indicating whether the dialog should close when the Apply command is executed.
    /// </summary>
    public bool CloseOnApply { get; set; } = true;

    /// <summary>
    /// Gets the title of the dialog.
    /// </summary>
    public virtual string DialogTitle => string.Empty;

    public DialogViewModelBase()
    {
        Dialog = new SukiDialog();
    }

    public DialogViewModelBase(ISukiDialog dialog)
    {
        Dialog = dialog;
    }

    [RelayCommand]
    public void CloseDialog()
    {
        Dialog.Dismiss();
    }

    protected virtual Task<bool> ApplyInternal()
    {
        return Task.FromResult(true);
    }

    [RelayCommand]
    public async Task Apply()
    {
        if(!Validate()) return;
        if(!await ApplyInternal()) return;
        if (CloseOnApply) CloseDialog();
    }
}