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
    private readonly ILoginService _loginService;
    
    public LoginViewModel(INavigationService navigationService, ILoginService loginService)
    {
            _navigationService = navigationService;
            _loginService = loginService;
    }
    
    [ObservableProperty]
    private string? _username;

    [ObservableProperty] 
    private string? _noAccountText;

    [RelayCommand]
    private async Task Login(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            if (await _loginService.Login(Username, passwordBox.Password))
            {
                if (_loginService.LoggedInUser?.Role == "Admin")
                    _navigationService.OpenWindow<AdminWindow>();
                
                else
                    _navigationService.OpenWindow<ClientWindow>();
                
                _navigationService.CloseWindow<LoginWindow>();
            }
            
            Username = string.Empty;
            passwordBox.Password = string.Empty;
            NoAccountText = "Username or password is incorrect.";
        }
    }

    [RelayCommand]
    private void Signup()
    {
        _navigationService.OpenWindow<SignupWindow>();
        _navigationService.CloseWindow<LoginWindow>();
    }
}