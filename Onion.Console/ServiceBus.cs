using System;
using System.Collections.Generic;
using Onion.Domain;

namespace Onion.Console
{
    public class ServiceBus
    {
        public void Send(BoundedContext boundedContext)
        {
            foreach (var @event in boundedContext.Events)
            {
            }
        }

        public ServiceBus Take(Func<BoundedContext, IReadOnlyCollection<EventNotification>> events)
        {
            return this;
        }

        public ServiceBus WithAcknowledgement()
        {
            return this;
        }
    }
}