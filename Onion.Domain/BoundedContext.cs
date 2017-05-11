using System;
using System.Threading.Tasks;
using Onion.Domain.Base;
using Yahoo.Data;

namespace Onion.Domain
{
    public class BoundedContext : DomainBase<EntityNotification, EventNotification>
    {
        private BoundedContext()
        {
        }

        public bool Process()
        {
            var currencies = RestInvokers[typeof (CurrenciesDto)].Invoke(1);

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