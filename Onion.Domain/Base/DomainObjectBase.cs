using System;

namespace Onion.Domain.Base
{
    public class DomainObjectBase
    {
        private readonly Guid _identity;

        protected DomainObjectBase()
        {
            _identity = Guid.NewGuid();
        }

        // ReSharper disable once ConvertToAutoProperty
        public Guid Identity => _identity;
    }
}