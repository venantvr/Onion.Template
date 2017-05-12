using System;
using System.Linq;
using System.Transactions;
using Onion.Console.Persistence;
using Onion.Domain;
using Onion.Domain.Dtos;
using Onion.Domain.Notifications;
using Yahoo.Data;

namespace Onion.Console
{
    // Layer Application Service
    public class BootStrapper
    {
        private BoundedContext _boundedContext;
        // Not really a UoW, contains repos...
        private UnitOfWork<EntityNotification> _entitiesUoW;
        private UnitOfWork<EventNotification> _eventsUoW;
        private ServiceBus _serviceBus;

        // Bootstrap
        public BootStrapper()
        {
            // Private constructor + factory
            _boundedContext = BoundedContext.CreateUseCase();
        }

        public BootStrapper SetExternalDataSources()
        {
            _boundedContext = _boundedContext
                .WithInvokerAsync<CurrenciesDto>(new Service().RetrieveCurrenciesAsync)
                .WithInvoker<CurrenciesDto>(new Service().RetrieveCurrencies);

            return this;
        }

        public BootStrapper SetInternalStorage()
        {
            _eventsUoW = new UnitOfWork<EventNotification>(new SqlServerLogic()).Take(d => d.Events);
            _entitiesUoW = new UnitOfWork<EntityNotification>(new MongoDbLogic()).Take(d => d.Entities);

            return this;
        }

        public BootStrapper SetServiceBus()
        {
            _serviceBus = new ServiceBus()
                .Take(d => d.Events.Select(e => e.Uid).ToList().AsReadOnly());

            return this;
        }

        public void Execute(Func<BoundedContext, bool> func)
        {
            // Breaks if any error occurs
            try
            {
                // Starting the process
                func.Invoke(_boundedContext);

                // Creating a TransactionScope (if possible, more stuff is needed depending on storage runtimes...)
                // Transaction à porter dans les UoW
                using (var transaction = new TransactionScope())
                {
                    if (_entitiesUoW.Persist(_boundedContext) && _eventsUoW.Persist(_boundedContext))
                    {
                        transaction.Complete();
                    }
                }

                // If everything is OK, returns to the database and play persisted notifications...
                _serviceBus
                    .WithAcknowledgement()
                    .Send(_boundedContext);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
            }
        }
    }
}