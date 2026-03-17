using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;

namespace SupportHub.Core.Services;

public class ClientService : IClientService
{
    private readonly IRepository<Ticket> _ticketRepository;

    public ClientService(IRepository<Ticket> ticketRepository)
    {
        _ticketRepository = ticketRepository; 
    }
    public async Task<List<Ticket>> GetTicketsByIdAsync(Guid? clientId, CancellationToken cancellationToken)
    {
        if(clientId == null)
            return new List<Ticket>();

        var tickets = await _ticketRepository.GetAllAsync(cancellationToken);

        var ticketsById = tickets.Where(t => t.ClientId == clientId).ToList();

        return ticketsById;
    }
    public Ticket CreateTicket(string type, string problemDescription, Guid clientId)
    {
        var newTicket = new Ticket()
        {
            Type = type,
            ProblemDescription = problemDescription,
            Status = "Open",
            Id = new Guid(),
            ClientId = clientId,
            DateCreated = DateTime.Now
        };

        return newTicket;
    }
    public async Task AddTicketDbAsync(Ticket ticket)
    {
        var listDbTickets = await _ticketRepository.GetAllAsync();

        listDbTickets.Add(ticket);

        await _ticketRepository.SaveAsync(listDbTickets);
    }

}