using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportHub.Core.Interfaces;
using SupportHub.WPF.View;

namespace SupportHub.WPF.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly Window _window;
    
    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    [ObservableProperty]
    private string? _username;

    [ObservableProperty] 
    private string? _noAccountText;

    [RelayCommand]
    private void Login(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            var password = passwordBox.Password;
            if (Username == "admin" && password == "admin")
            {
                _navigationService.OpenWindow<AdminWindow>();
                _navigationService.CloseWindow<LoginWindow>();
            }
        }
        NoAccountText = "Username or password is incorrect.";
    }

    [RelayCommand]
    private void Signup()
    {
        _navigationService.OpenWindow<SignupWindow>();
        _navigationService.CloseWindow<LoginWindow>();
    }
}