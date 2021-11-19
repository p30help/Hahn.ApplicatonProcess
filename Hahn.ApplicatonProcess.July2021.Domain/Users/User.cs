using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Domain.Common;
using Hahn.ApplicationProcess.July2021.Domain.Exceptions;
using Hahn.ApplicationProcess.July2021.Domain.Users.DomainServices;
using Hahn.ApplicationProcess.July2021.Domain.Users.Events;
using Hahn.ApplicationProcess.July2021.Domain.ValueObjects;

namespace Hahn.ApplicationProcess.July2021.Domain.Users
{
    public class User : AggregateRoot
    {
        public int Id { get; private set; }

        public HumanAge Age { get; private set; }

        public HumanName FirstName { get; private set; }

        public HumanName LastName { get; private set; }

        public string Address { get; private set; }

        public EmailField Email { get; private set; }


        private List<Asset> _assets = new List<Asset>();
        public IReadOnlyCollection<Asset> Assets
        {
            get
            {
                return _assets.AsReadOnly();
            }
        }

        private User() { }

        public static User Create(int id, HumanName firstName, HumanName lastName,
            HumanAge age, EmailField email, string street, string houseNum, int postalCode)
        {
            var item = new User()
            {
                Id = id
            };

            item.SetFirstName(firstName);
            item.SetLastName(lastName);
            item.SetAge(age);
            item.SetEmail(email);
            item.SetAddress(street, houseNum, postalCode);

            // event 
            var userCreated = new UserCreated()
            {
                UserId = item.Id
            };
            item.AddEvent(userCreated);

            return item;
        }

        public void SetFirstName(HumanName value)
        {
            this.FirstName = value;
        }

        public void SetLastName(HumanName value)
        {
            this.LastName = value;
        }

        public void SetAge(HumanAge value)
        {
            this.Age = value;
        }

        public void SetEmail(EmailField value)
        {
            this.Email = value;
        }

        public void SetAddress(string street, string houseNum, int postalCode)
        {
            this.Address = $"{street} - {houseNum} - {postalCode}";
        }

        public async Task AddAsset(int id, string key, string symbol, string name, IIsAssetValidated isAssetIdValidated)
        {
            if (_assets.Any(x => x.Key == key))
            {
                throw new InvalidEntityStateException("This asset has already been added to this user");
            }

            var item = await Asset.Create(this, id, key, symbol, name, isAssetIdValidated);

            _assets.Add(item);
        }

        public void RemoveAsset(Asset asset)
        {
            var item = _assets.FirstOrDefault(x => x.Id == asset.Id);
            if (item == null)
            {
                throw new InvalidEntityStateException("This asset doesn't exist in this object");
            }

            _assets.Remove(item);
        }

        public void ClearAssets()
        {
            _assets.Clear();
        }

        public void Remove()
        {
        }
    }
}