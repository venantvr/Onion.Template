using System;
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
        private BoundedContext _domain;
        private UnitOfWork<EntityNotification> _entityUow;
        private UnitOfWork<EventNotification> _eventUow;
        private Func<BoundedContext, bool> _func;
        private ServiceBus _serviceBus;

        // Bootstrap
        public BootStrapper()
        {
            _domain = BoundedContext.CreateUseCase();

            using (var transaction = new TransactionScope())
            {
                try
                {
                    _func.Invoke(_domain);

                    if (_entityUow.Persist(_domain) && _eventUow.Persist(_domain))
                    {
                        _serviceBus
                            .WithAcknowledgement()
                            .Send(_domain);

                        transaction.Complete();
                    }
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine(exception);
                }
            }
        }

        public BootStrapper SetExternalDataSources()
        {
            _domain = _domain
                .WithInvokerAsync<CurrenciesDto>(new Service().RetrieveCurrenciesAsync)
                .WithInvoker<CurrenciesDto>(new Service().RetrieveCurrencies);

            return this;
        }

        public BootStrapper SetInternalStorage()
        {
            _eventUow = new UnitOfWork<EventNotification>(new SqlServerLogic()).Take(d => d.Events);
            _entityUow = new UnitOfWork<EntityNotification>(new MongoDbLogic()).Take(d => d.Entities);

            return this;
        }

        public BootStrapper SetServiceBus()
        {
            _serviceBus = new ServiceBus()
                .Take(d => d.Events);

            return this;
        }

        public void Execute(Func<BoundedContext, bool> func)
        {
            _func = func;
        }
    }
}