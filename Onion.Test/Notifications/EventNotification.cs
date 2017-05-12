using System;
using Onion.Test.Base;

namespace Onion.Test.Notifications
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EventNotification : NotificationBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid Uid { get; set; } // Client-side for idempotence and other reasons...
    }
}