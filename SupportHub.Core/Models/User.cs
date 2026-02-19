using System.ComponentModel.DataAnnotations;

namespace SupportHub.Core.Models;

public class User : DomainModel
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required Guid Id { get; init; }
    public required string Role { get; init; }
    public string? Email { get; set; }
}