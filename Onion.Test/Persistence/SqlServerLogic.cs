using System.Collections.ObjectModel;

namespace Onion.Test
{
    public class SqlServerLogic : IPersistenceLogic
    {
        public bool Store(ReadOnlyCollection<NotificationBase> items)
        {
            return true;
        }
    }
}