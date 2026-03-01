namespace SupportHub.Core.Models;

public class Ticket : DomainModel
{
    public required string Type { get; init; }
    public required string ProblemDescription { get; init; }
    public required string Status { get; init; }
    public required Guid Id { get; init; }
    public required Guid ClientId { get; init; }
    public required Guid AdminId { get; init; }
    public required DateTime DateCreated { get; init; }
    public DateTime DateFinished { get; init; }
    
}