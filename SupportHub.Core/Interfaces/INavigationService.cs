namespace SupportHub.Core.Interfaces;

public interface INavigationService
{
    public void OpenWindow<TWindow>() where TWindow : class;
    public void CloseWindow<TWindow>() where TWindow : class;
}