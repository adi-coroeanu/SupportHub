using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;
using SupportHub.WPF.Stores;
using SupportHub.WPF.View;

namespace SupportHub.WPF.ViewModel;

public partial class ClientViewModel : ObservableObject
{
    private readonly IClientService _clientService;
    private readonly INavigationService _navigationService;
    private readonly NavigationStore _navigationStore;
    
    public ClientViewModel(IClientService clientService, INavigationService navigationService, NavigationStore navigationStore)
    {
        _clientService = clientService;
        _navigationService = navigationService;
        _navigationStore = navigationStore;
    }

    public ClientViewModel()
    {
        
    }
    
    public string? ActiveUsername => _navigationStore.ActiveUser?.Username;
    
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(ProblemTextLength))]
    private string? _problemText;
    
    public int ProblemTextLength => ProblemText?.Length ?? 0;

    [RelayCommand]
    private void Logout()
    {
        _navigationService.OpenWindow<LoginWindow>();
        _navigationService.CloseWindow<ClientWindow>();
    }

    public ObservableCollection<Ticket> Tickets { get; } = new ObservableCollection<Ticket>();
}