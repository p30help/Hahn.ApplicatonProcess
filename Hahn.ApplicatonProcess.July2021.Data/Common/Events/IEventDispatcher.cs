using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Domain.Common.Events;

namespace Hahn.ApplicationProcess.July2021.Data.Common.Events
{
    public interface IEventDispatcher
    {
        Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;

        Task PublishDomainEventsAsync<TDomainEvent>(IEnumerable<TDomainEvent> @events) where TDomainEvent : class, IDomainEvent;

    }
}
