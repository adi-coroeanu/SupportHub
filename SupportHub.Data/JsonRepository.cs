namespace SupportHub.Data;

using SupportHub.Core.Models;
using SupportHub.Core.Interfaces;
using System.Text.Json;

public class JsonRepository<T> : IRepository<T> where T : DomainModel
{
    private readonly string _filePath;
    public JsonRepository()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{typeof(T).Name.ToLower()}s.json");
        
        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "[]");
        // Console.WriteLine(_filePath);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var stringJson = await File.ReadAllTextAsync(_filePath, cancellationToken);
        return JsonSerializer.Deserialize<List<T>>(stringJson) ?? new List<T>();
    }
    
    public async Task SaveAsync(List<T> items,  CancellationToken cancellationToken = default)
    {
        var stringJson = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, stringJson,  cancellationToken);
    }
}