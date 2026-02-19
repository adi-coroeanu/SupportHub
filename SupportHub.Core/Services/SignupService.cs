using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;

namespace SupportHub.Core.Services;

public class SignupService : ISignupService
{
    private readonly IRepository<User> _usersRepository;
    private readonly IRepository<AdminCode> _adminCodeRepository;
    
    public SignupService(IRepository<User> usersRepository,  IRepository<AdminCode> adminCodeRepository)
    {
        _usersRepository = usersRepository;
        _adminCodeRepository = adminCodeRepository;
    }
    
    public async Task<bool> ExistingUsername(string? username, CancellationToken cancellationToken=default)
    {
        if (username == null)
            return false;
        
        var users =  await _usersRepository.GetAllAsync(cancellationToken);
        Console.WriteLine(users.Any(u => u.Username == username));

        return users.Any(u => u.Username == username);
    }

    public bool ValidPasswordFormat(string password)
    {
        bool hasUpperCase = password.Any(char.IsUpper);
        bool hasLowerCase = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecial = password.Any(p => !char.IsLetterOrDigit(p));
        
        if(password.Length > 8 && hasUpperCase && hasLowerCase && hasDigit && hasSpecial)
            return true;
        
        return false;
    }

    public bool ValidEmailFormat(string? email, CancellationToken cancellationToken=default)
    {
        return true;
    }

    public async Task<bool> Signup(string? username, string password, string repassword, string? email, string? code = null)
    {
        string role;

        if (!ValidPasswordFormat(password) || password != repassword || !ValidEmailFormat(email) || await ExistingUsername(username)) 
            return false;
        
        if (code == null)
            role = "Client";
        else if (await VerifyAdminCode(code))
            role = "Admin";
        else
            return false;
        
        var users = await _usersRepository.GetAllAsync();

        var newUser = new User()
        {
            Username = username,
            Password = password,
            Id = Guid.NewGuid(),
            Role = role,
            Email = email,
        };
            
        users.Add(newUser);
        await _usersRepository.SaveAsync(users);

        return true;
    }
    
    private async Task<bool> VerifyAdminCode(string code)
        => (await _adminCodeRepository.GetAllAsync()).Any(ac => ac.Code == code);
}