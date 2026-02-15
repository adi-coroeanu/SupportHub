using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;

namespace SupportHub.Core.Services;

public class LoginService
{
    private readonly IRepository<User> _userRepository;
    
    public LoginService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Login(string username, string password)
    {
        var users = _userRepository.GetAllAsync().Result;
        
    }
}