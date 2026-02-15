namespace SupportHub.Core.Models;

public class Ticket : DomainModel
{
    public required string ProblemDescription { get; init; }
    public required string Status { get; init; }
    public required Guid Id { get; init; }
    public required Guid OperatorId { get; init; }
    public required DateTime DateCreated { get; init; }
    public DateTime DateFinished { get; init; }
}