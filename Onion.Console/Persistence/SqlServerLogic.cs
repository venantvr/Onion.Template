using System.Collections.ObjectModel;
using Onion.Domain.Base;

namespace Onion.Console.Persistence
{
    public class SqlServerLogic : IPersistenceLogic
    {
        public bool Store(ReadOnlyCollection<NotificationBase> items)
        {
            return true;
        }
    }
}