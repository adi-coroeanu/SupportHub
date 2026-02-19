using SupportHub.Core.Interfaces;

namespace SupportHub.WPF.Services;

public class DialogService : IDialogService
{
    public void ShowMessage(string message)
    {
        System.Windows.MessageBox.Show(message);
    }
}