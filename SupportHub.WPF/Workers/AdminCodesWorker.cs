using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SupportHub.Core.Configurations;
using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;

namespace SupportHub.WPF.Workers;

public class AdminCodesWorker : BackgroundService
{
    private readonly IRepository<AdminCode> _adminCodesRepository;
    private readonly IOptionsMonitor<AdminCodesWorkerSettings> _config;

    public AdminCodesWorker(IRepository<AdminCode> adminCodesRepository, IOptionsMonitor<AdminCodesWorkerSettings> config)
    {
        _adminCodesRepository = adminCodesRepository;
        _config = config;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var codes = await _adminCodesRepository.GetAllAsync(stoppingToken);
            var expiredCodes = codes.Where(c => (DateTime.Now-c.DateCreated).TotalMinutes >= _config.CurrentValue.ExpirationTimeMinutes).ToList();
            
            codes.RemoveAll(expiredCodes.Contains);
            
            await _adminCodesRepository.SaveAsync(codes, stoppingToken);
            
            await Task.Delay(TimeSpan.FromMinutes(_config.CurrentValue.RefreshingTimeMinutes), stoppingToken);
        }
    }
}