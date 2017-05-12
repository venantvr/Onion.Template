using System;
using System.Collections.Generic;
using Onion.Domain;

namespace Onion.Console
{
    public class ServiceBus
    {
        private bool _acknowledgement;
        private Func<BoundedContext, IReadOnlyCollection<Guid>> _eventUids;

        public void Send(BoundedContext boundedContext)
        {
            // ReSharper disable once UnusedVariable
            foreach (var @event in _eventUids.Invoke(boundedContext))
            {
                if (_acknowledgement)
                {
                    // Retrieve from database...
                    // Send
                }
            }
        }

        public ServiceBus Take(Func<BoundedContext, IReadOnlyCollection<Guid>> eventUids)
        {
            _eventUids = eventUids;

            return this;
        }

        public ServiceBus WithAcknowledgement()
        {
            _acknowledgement = true;

            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public ServiceBus WithoutAcknowledgement()
        {
            _acknowledgement = false;

            return this;
        }
    }
}