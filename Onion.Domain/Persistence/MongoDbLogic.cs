using System.Collections.ObjectModel;

namespace Onion.Test
{
    public class MongoDbLogic : IPersistenceLogic
    {
        public bool Store(ReadOnlyCollection<NotificationBase> items)
        {
            return true;
        }
    }
}