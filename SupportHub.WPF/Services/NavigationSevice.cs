using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Core.Interfaces;

namespace SupportHub.WPF.Services;

public class NavigationSevice : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    
    public NavigationSevice(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void OpenWindow<TWindow>() where TWindow : class
    {
        var windowService = _serviceProvider.GetRequiredService<TWindow>();

        if (windowService is Window window)
        {
            window.Show();
        }
        else
        {
            throw new NotSupportedException($"Window type {windowService.GetType()} is not supported.");
        }
    }

    public void CloseWindow<TWindow>() where TWindow : class
    {
        var window = Application.Current.Windows.OfType<Window>().FirstOrDefault();

        if (window != null)
        {
            window.Close();
        }
        else
        {
            throw new NotSupportedException($"Window type {typeof(TWindow)} does not exist.");
        }
    }

    
}