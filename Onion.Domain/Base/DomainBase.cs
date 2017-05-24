using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Yahoo.Data.Interfaces;

namespace Onion.Domain.Base
{
    public class DomainBase<TEntities, TEvents>
    {
        private readonly List<TEntities> _entities;
        private readonly List<TEvents> _events;

        protected readonly Dictionary<Type, Func<int, IDtoObject>> RestInvokers = new Dictionary<Type, Func<int, IDtoObject>>();
        protected readonly Dictionary<Type, Func<int, Task<IDtoObject>>> RestInvokersAsync = new Dictionary<Type, Func<int, Task<IDtoObject>>>();

        protected DomainBase()
        {
            _entities = new List<TEntities>();
            _events = new List<TEvents>();
        }

        public ReadOnlyCollection<TEntities> Entities => _entities.AsReadOnly();
        public ReadOnlyCollection<TEvents> Events => _events.AsReadOnly();

        public DomainBase<TEntities, TEvents> WithInvokerAsync<T>(Func<int, Task<IDtoObject>> p) where T : IDtoObject
        {
            RestInvokersAsync.Add(typeof (T), p);

            return this;
        }

        public DomainBase<TEntities, TEvents> WithInvoker<T>(Func<int, IDtoObject> p) where T : IDtoObject
        {
            RestInvokers.Add(typeof (T), p);

            return this;
        }
    }
}