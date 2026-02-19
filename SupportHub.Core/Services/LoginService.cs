using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;

namespace SupportHub.Core.Services;

public class LoginService : ILoginService
{
    private readonly IRepository<User> _userRepository;
    public User? LoggedInUser { get; private set; }
    
    public LoginService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Login(string? username, string? password)
    {
        if (username is null || password is null)
            return false;
        
        var users = await _userRepository.GetAllAsync();
        var selectedUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (selectedUser != null)
        {
            LoggedInUser = selectedUser;
            return true;
        }

        return false;
    }
}