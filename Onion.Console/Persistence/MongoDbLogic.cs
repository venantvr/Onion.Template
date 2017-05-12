﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Onion.Domain.Base;

namespace Onion.Console.Persistence
{
    public class MongoDbLogic : IPersistenceLogic
    {
        public bool Store(ReadOnlyCollection<NotificationBase> items)
        {
            return true;
        }

        public List<NotificationBase> Retrieve()
        {
            return new List<NotificationBase>();
        }
    }
}