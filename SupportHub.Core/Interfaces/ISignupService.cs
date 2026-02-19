namespace SupportHub.Core.Interfaces;

public interface ISignupService
{
    Task<bool> ExistingUsername(string? username, CancellationToken cancellationToken=default);
    bool ValidPasswordFormat(string password);
    bool ValidEmailFormat(string? email,  CancellationToken cancellationToken=default);
    Task<bool> Signup(string? username, string password, string repassword, string? email, string? code = null);
}