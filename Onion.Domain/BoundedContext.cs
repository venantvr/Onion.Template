using Onion.Domain.Base;
using Onion.Domain.Dtos;
using Onion.Domain.Notifications;
using Yahoo.Data.Interfaces;

namespace Onion.Domain
{
    public class BoundedContext : DomainBase<DomainEntity, DomainEvent>
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
    }
}