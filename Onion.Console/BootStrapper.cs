using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Serialization;
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

        private static object Deserialize(string xml, Type toType)
        {
            using (Stream stream = new MemoryStream())
            {
                var data = Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new XmlSerializer(toType);
                return deserializer.Deserialize(stream);
            }
        }

        private static async Task<IDtoObject> RetrieveCurrenciesAsync(int i)
        {
            using (var client = new HttpClient())
            using (var response = client.GetAsync("http://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote"))
            using (var content = response.Result.Content)
            {
                var result = await content.ReadAsStringAsync();
                var dataObjects = Deserialize(result, typeof (CurrenciesList));
                return (CurrenciesList) dataObjects;
            }
        }

        private static IDtoObject RetrieveCurrencies(int i)
        {
            using (var client = new HttpClient())
            using (var response = client.GetAsync("http://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote"))
            using (var content = response.Result.Content)
            {
                var result = content.ReadAsStringAsync();
                var dataObjects = Deserialize(result.GetAwaiter().GetResult(), typeof (CurrenciesList));
                return (CurrenciesList) dataObjects;
            }
        }

        public BootStrapper SetExternalDataSources()
        {
            _domain = _domain
                .WithInvokerAsync<CurrenciesDto>(RetrieveCurrenciesAsync)
                .WithInvoker<CurrenciesDto>(RetrieveCurrencies);

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