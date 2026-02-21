using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportHub.Core.Interfaces;

namespace SupportHub.WPF.ViewModel;

public partial class AdminViewModel : ObservableObject
{
    private readonly IAdminCodeGeneratorService _adminCodeGeneratorService;
    
    public AdminViewModel(IAdminCodeGeneratorService adminCodeGeneratorService)
    {
        _adminCodeGeneratorService = adminCodeGeneratorService;
    }
    
    [ObservableProperty]
    private string? _codeText;

    [RelayCommand]
    private async Task GenerateAdminCode()
    {
        CodeText = await _adminCodeGeneratorService.GenerateCode();
    }
}