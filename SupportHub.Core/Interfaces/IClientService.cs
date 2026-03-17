using SupportHub.Core.Models;

namespace SupportHub.Core.Interfaces;

public interface IClientService
{
    Task<List<Ticket>> GetTicketsByIdAsync(Guid? clientId, CancellationToken cancellationToken);
    Ticket CreateTicket(string type, string problemDescription, Guid clientId);
    Task AddTicketDbAsync(Ticket ticket);

}