using System;
using System.Threading.Tasks;
using Onion.Test.Base;
using Onion.Test.Dtos;
using Onion.Test.Notifications;
using Yahoo.Data.Interfaces;

namespace Onion.Test
{
    public class BoundedContext : DomainBase<EntityNotification, EventNotification>
    {
        private IDtoObject _currencies;

        private BoundedContext()
        {
        }

        public bool Process()
        {
            _currencies = RestInvokers[typeof (CurrenciesDto)].Invoke(1);

            return true;
        }

        public static BoundedContext CreateUseCase()
        {
            return new BoundedContext();
        }

        public BoundedContext WithInvokerAsync<T>(Func<int, Task<IDtoObject>> p) where T : IDtoObject
        {
            RestInvokersAsync.Add(typeof (T), p);

            return this;
        }

        public BoundedContext WithInvoker<T>(Func<int, IDtoObject> p) where T : IDtoObject
        {
            RestInvokers.Add(typeof (T), p);

            return this;
        }
    }
}