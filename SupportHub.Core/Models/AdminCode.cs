namespace SupportHub.Core.Models;

public class AdminCode : DomainModel
{
    public required string Code { get; init; }
    public required DateTime DateCreated { get; init; }
}