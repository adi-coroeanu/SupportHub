using System.Windows;
using SupportHub.WPF.ViewModel;

namespace SupportHub.WPF.View;

public partial class LoginWindow : Window
{
    public LoginWindow(LoginViewModel viewModel)
    {
        InitializeComponent();
        
        DataContext = viewModel;
    }
}