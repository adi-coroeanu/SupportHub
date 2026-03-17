using System.Windows;
using System.Windows.Navigation;
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
    
    public async Task OpenWindow<TWindow>() where TWindow : class
    {
        var windowService = _serviceProvider.GetRequiredService<TWindow>();

        if (windowService is Window window)
        {
            var viewModel = window.DataContext;

            if (viewModel != null)
            {
                Type viewModelType = viewModel.GetType();
                var methodAsyncInit = viewModelType.GetMethod("AsyncInit");

                if (methodAsyncInit != null)
                {
                    //var viewModelSevice = _serviceProvider.GetRequiredService(viewModelType);

                    var result = methodAsyncInit.Invoke(viewModel, null);

                    if (result is Task process)
                        await process;
                }
            }
            
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