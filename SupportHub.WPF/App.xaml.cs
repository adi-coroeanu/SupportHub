using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;
using SupportHub.Data;
using SupportHub.WPF.ViewModel;
using SupportHub.WPF.Services;
using SupportHub.WPF.View;

namespace SupportHub.WPF;

public partial class App : Application
{
    private IHost _host;
    
    public App()
    {
        _host = new HostBuilder()
            .ConfigureServices((context, services) =>
            {
                //Windows
                services.AddTransient<LoginWindow>();
                services.AddTransient<SignupWindow>();
                services.AddTransient<AdminWindow>();
                
                //ViewModels
                services.AddTransient<LoginViewModel>();
                
                //Models
                services.AddSingleton<INavigationService, NavigationSevice>();
                
                //Data
                services.AddSingleton<IRepository<User>, JsonRepository<User>>();
                services.AddSingleton<IRepository<Ticket>, JsonRepository<Ticket>>();
                services.AddSingleton<IRepository<AdminCode>, JsonRepository<AdminCode>>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.StartAsync();
        
        var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
        loginWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            _host.StopAsync();
        }
    }
}