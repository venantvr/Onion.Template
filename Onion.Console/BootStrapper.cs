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
        private DatabaseContext<DomainEntity> _entitiesContext;
        private DatabaseContext<DomainEvent> _eventsContext;
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
            // Loads BC definitions that will be later persisted...
            _eventsContext = new DatabaseContext<DomainEvent>(new SqlServerLogic()).Take(d => d.Events);
            _entitiesContext = new DatabaseContext<DomainEntity>(new MongoDbLogic()).Take(d => d.Entities);

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
            // Breaks if a critical error occurs
            try
            {
                // Starts the process
                func.Invoke(_boundedContext);

                // Creates a TransactionScope (if possible, more stuff is needed depending on storage runtimes...)
                // Transactions should be the responsability of each context
                using (var transaction = new TransactionScope())
                {
                    if (_entitiesContext.Persist(_boundedContext) && _eventsContext.Persist(_boundedContext))
                    {
                        transaction.Complete();
                    }
                }

                // If everything is OK, returns to the database and play persisted notifications...
                // TODO : Separate Storage from Events
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