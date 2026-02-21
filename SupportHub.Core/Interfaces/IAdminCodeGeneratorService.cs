namespace SupportHub.Core.Interfaces;

public interface IAdminCodeGeneratorService
{
    Task<string> GenerateCode(int length = 8);
}