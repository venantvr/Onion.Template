using System;
using Onion.Domain.Base;

namespace Onion.Domain.Notifications
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DomainEvent : DomainObjectBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid Uid { get; set; } // Client-side for idempotence and other reasons...
    }
}