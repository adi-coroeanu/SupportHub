using System.Windows;
using SupportHub.WPF.ViewModel;

namespace SupportHub.WPF.View;

public partial class ClientWindow : Window
{
    public ClientWindow(ClientViewModel viewModel)
    {
        InitializeComponent();
        
        DataContext = viewModel;
    }
}