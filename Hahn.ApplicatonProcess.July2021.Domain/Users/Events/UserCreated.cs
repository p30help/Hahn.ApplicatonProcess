using Hahn.ApplicationProcess.July2021.Domain.Common.Events;

namespace Hahn.ApplicationProcess.July2021.Domain.Users.Events
{
    public class UserCreated : IDomainEvent
    {
        public int UserId { get; set; }
    }
}
