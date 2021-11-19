using Hahn.ApplicationProcess.July2021.Domain.Common.Events;
using System.Collections.Generic;
using System.Linq;

namespace Hahn.ApplicationProcess.July2021.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _events;
        protected AggregateRoot() => _events = new List<IDomainEvent>();
        
        protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

        public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();

        public void ClearEvents() => _events.Clear();
    }
}
