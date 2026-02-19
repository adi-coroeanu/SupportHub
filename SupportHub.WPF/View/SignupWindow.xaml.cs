using System.Windows;
using SupportHub.WPF.ViewModel;

namespace SupportHub.WPF.View;

public partial class SignupWindow : Window
{
    public SignupWindow(SignupViewModel viewModel)
    {
        InitializeComponent();
        
        DataContext = viewModel;
    }
}