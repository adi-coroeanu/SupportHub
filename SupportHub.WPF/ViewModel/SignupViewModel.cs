using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportHub.Core.Interfaces;
using SupportHub.WPF.View;

namespace SupportHub.WPF.ViewModel;

public partial class SignupViewModel : ObservableObject
{
    private readonly ISignupService _signupService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private CancellationTokenSource? _cancellationTokenSource;
    
    public SignupViewModel(ISignupService signupService, IDialogService dialogService,  INavigationService navigationService)
    {
        _signupService = signupService;
        _dialogService = dialogService;
        _navigationService = navigationService;
    }

    [ObservableProperty] 
    private string? _username;

    [ObservableProperty]
    private bool _isAdminChecked;
    
    [ObservableProperty]
    private bool _userExists;

    [ObservableProperty] 
    private string? _email;

    [ObservableProperty] 
    private bool _validEmailFormat;

    [RelayCommand]
    public async Task Signup(object[] parameters)
    {
        if (parameters is [PasswordBox passwordBox, PasswordBox repasswordBox])
        {
            var password = passwordBox.Password;
            var repassword = repasswordBox.Password;

            if (await _signupService.Signup(Username, password, repassword, Email))
            {
                _dialogService.ShowMessage("Signup successful");
                _navigationService.OpenWindow<LoginWindow>();
                _navigationService.CloseWindow<SignupWindow>();
            }
            else
                _dialogService.ShowMessage("Signup failed");
        }
    }

    partial void OnUsernameChanged(string? value)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        
        _ = VerifyUsername(value, _cancellationTokenSource.Token);
    }
    
    

    private async Task VerifyUsername(string? username, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _signupService.ExistingUsername(username, cancellationToken);

            UserExists = result;
        }
        catch (OperationCanceledException) { }
    }
}