using System.Collections.Generic;
using System.Collections.ObjectModel;
using Onion.Domain.Base;

namespace Onion.Console.Persistence
{
    public class MongoDbLogic : IPersistenceLogic
    {
        public bool Store(ReadOnlyCollection<DomainObjectBase> items)
        {
            return true;
        }

        public List<DomainObjectBase> Retrieve()
        {
            return new List<DomainObjectBase>();
        }
    }
}