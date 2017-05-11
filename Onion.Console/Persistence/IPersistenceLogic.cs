using System.Collections.ObjectModel;
using Onion.Domain.Base;

namespace Onion.Console.Persistence
{
    public interface IPersistenceLogic
    {
        bool Store(ReadOnlyCollection<NotificationBase> items);
    }
}