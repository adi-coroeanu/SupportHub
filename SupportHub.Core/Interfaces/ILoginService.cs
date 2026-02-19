using SupportHub.Core.Models;

namespace SupportHub.Core.Interfaces;

public interface ILoginService
{
    public Task<bool> Login(string? username, string? password);
    public User? LoggedInUser { get; }
}