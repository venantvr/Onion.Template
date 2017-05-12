using System.Collections.ObjectModel;

namespace Onion.Test
{
    public interface IPersistenceLogic
    {
        bool Store(ReadOnlyCollection<NotificationBase> items);
    }
}