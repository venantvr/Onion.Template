using System;
using System.Collections.Generic;
using Onion.Domain;
using Onion.Domain.Notifications;

namespace Onion.Console
{
    public class ServiceBus
    {
        private bool _acknowledgement;

        public void Send(BoundedContext boundedContext)
        {
            foreach (var @event in boundedContext.Events)
            {
                if (_acknowledgement)
                {
                }
            }
        }

        public ServiceBus Take(Func<BoundedContext, IReadOnlyCollection<EventNotification>> events)
        {
            return this;
        }

        public ServiceBus WithAcknowledgement()
        {
            _acknowledgement = true;

            return this;
        }

        public ServiceBus WithoutAcknowledgement()
        {
            _acknowledgement = false;

            return this;
        }
    }
}