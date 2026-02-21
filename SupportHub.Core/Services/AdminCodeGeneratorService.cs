using System.Security.Cryptography;
using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;

namespace SupportHub.Core.Services;

public class AdminCodeGeneratorService : IAdminCodeGeneratorService
{
    private readonly IRepository<AdminCode> _adminCodeRepository;
    public AdminCodeGeneratorService(IRepository<AdminCode> adminCodeRepository)
    {
        _adminCodeRepository = adminCodeRepository;
    }

    public async Task<string> GenerateCode(int length = 8)
    {
        var enabledCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var code = new char[length];

        for (int i = 0; i < length; i++)
        {
            var randomIdx = RandomNumberGenerator.GetInt32(enabledCharacters.Length);
            code[i] = enabledCharacters[randomIdx];
        }
        
        var codeString = new string(code);

        var allAdminCodes = await _adminCodeRepository.GetAllAsync();

        var newAdminCode = new AdminCode()
        {
            Code = codeString,
            DateCreated = DateTime.Now
        };
        
        allAdminCodes.Add(newAdminCode);
        
        await _adminCodeRepository.SaveAsync(allAdminCodes);
        
        return codeString;
    }
}