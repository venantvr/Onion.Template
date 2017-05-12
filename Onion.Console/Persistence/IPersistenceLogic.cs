﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Onion.Domain.Base;

namespace Onion.Console.Persistence
{
    public interface IPersistenceLogic
    {
        // ReSharper disable once UnusedParameter.Global
        bool Store(ReadOnlyCollection<NotificationBase> items);
        List<NotificationBase> Retrieve();
    }
}