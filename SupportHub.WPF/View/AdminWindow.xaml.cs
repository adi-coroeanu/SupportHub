using System.Windows;
using SupportHub.WPF.ViewModel;

namespace SupportHub.WPF.View;

public partial class AdminWindow : Window
{
    public AdminWindow(AdminViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}