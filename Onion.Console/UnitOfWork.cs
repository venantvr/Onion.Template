using System;
using System.Collections.ObjectModel;
using System.Linq;
using Onion.Console.Persistence;
using Onion.Domain;
using Onion.Domain.Base;

namespace Onion.Console
{
    internal class UnitOfWork<TNotification> : IDisposable
    {
        private readonly IPersistenceLogic _logic;
        private Func<BoundedContext, ReadOnlyCollection<TNotification>> _domainFunc;
        private ReadOnlyCollection<TNotification> _domainNotifications;

        public UnitOfWork(IPersistenceLogic logic)
        {
            _logic = logic;
        }

        public void Dispose()
        {
        }

        internal bool Persist(BoundedContext boundedContext)
        {
            _domainNotifications = _domainFunc.Invoke(boundedContext);

            return _logic.Store(_domainNotifications.Cast<NotificationBase>().ToList().AsReadOnly());
        }

        public UnitOfWork<TNotification> Take(Func<BoundedContext, ReadOnlyCollection<TNotification>> func)
        {
            _domainFunc = func;

            return this;
        }
    }
}