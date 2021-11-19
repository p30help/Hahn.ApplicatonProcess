using System.Collections.Generic;
using Hahn.ApplicationProcess.July2021.Domain.Common;

namespace Hahn.ApplicationProcess.July2021.Domain.Users
{
    public class UserDto : AggregateRoot
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }


        public List<AssetDto> Assets
        {
            get;
            set;
        }

    }
}