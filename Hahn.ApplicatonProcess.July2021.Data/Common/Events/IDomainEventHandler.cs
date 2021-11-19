using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Domain.Common.Events;

namespace Hahn.ApplicationProcess.July2021.Data.Common.Events
{
    public interface IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        Task Handle(TDomainEvent Event);
    }

}
