using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Markup;
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
    private readonly CancellationTokenSource _cancellationTokenSource;

    public ClientViewModel(IClientService clientService, INavigationService navigationService, NavigationStore navigationStore)
    {
        _clientService = clientService;
        _navigationService = navigationService;
        _navigationStore = navigationStore;
        _cancellationTokenSource = new();
    }

    public ObservableCollection<Ticket> ClientTickets { get; } = new();

    public async Task AsyncInit()
    {
        var allClientTickets = await _clientService.GetTicketsByIdAsync(_navigationStore.ActiveUser?.Id, _cancellationTokenSource.Token);

        foreach (var ticket in allClientTickets)
        {
            ClientTickets.Add(ticket);
        }
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

    [ObservableProperty]
    private string? _typeProblem;

    [RelayCommand]
    private async Task AddTicket()
    {
        var newTicket = _clientService.CreateTicket(TypeProblem, ProblemText, _navigationStore.ActiveUser.Id);

        ClientTickets.Add(newTicket);

        await _clientService.AddTicketDbAsync(newTicket);
    }

}