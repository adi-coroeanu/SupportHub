using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupportHub.Core.Interfaces;
using SupportHub.Core.Models;
using SupportHub.Core.Services;
using SupportHub.Data;
using SupportHub.WPF.Convertors;
using SupportHub.WPF.ViewModel;
using SupportHub.WPF.Services;
using SupportHub.WPF.View;
using SupportHub.WPF.Workers;

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
                services.AddTransient<ClientWindow>();
                
                //ViewModels
                services.AddTransient<LoginViewModel>();
                services.AddTransient<SignupViewModel>();
                services.AddTransient<AdminViewModel>();
                
                //Models
                services.AddSingleton<INavigationService, NavigationSevice>();
                services.AddTransient<ILoginService, LoginService>();
                services.AddTransient<ISignupService, SignupService>();
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<IAdminCodeGeneratorService, AdminCodeGeneratorService>();
                
                //Data
                services.AddSingleton<IRepository<User>, JsonRepository<User>>();
                services.AddSingleton<IRepository<Ticket>, JsonRepository<Ticket>>();
                services.AddSingleton<IRepository<AdminCode>, JsonRepository<AdminCode>>();
                
                //Converters
                services.AddSingleton<IMultiValueConverter, ToArrayConverter>();
                
                //Workers
                services.AddHostedService<AdminCodesWorker>();
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