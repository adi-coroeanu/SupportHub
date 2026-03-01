using SupportHub.Core.Models;

namespace SupportHub.WPF.Stores;

public class NavigationStore
{
    public User? ActiveUser
    {
        get
        {
            if (field == null)
                throw new NullReferenceException();
            return field;
        }
        set;
    }
}