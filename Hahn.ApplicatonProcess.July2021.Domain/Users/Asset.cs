using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Domain.Common;
using Hahn.ApplicationProcess.July2021.Domain.Exceptions;
using Hahn.ApplicationProcess.July2021.Domain.Users.DomainServices;

namespace Hahn.ApplicationProcess.July2021.Domain.Users
{
    public class Asset : Entity
    {
        public int Id { get; private set; }

        public string Key { get; private set; }

        public string Symbol { get; private set; }

        public string Name { get; private set; }

        public User User { get; private set; }

        private Asset() { }

        public static async Task<Asset> Create(User user, int id, string key, string symbol, string name,
            IIsAssetValidated isAssetIdValidated)
        {
            if (user == null)
            {
                throw new InvalidEntityStateException("User must be entered");
            }

            if (await isAssetIdValidated.Execute(key, symbol, name) == false)
            {
                throw new InvalidEntityStateException("Asset id is not valid");
            }

            var item = new Asset()
            {
                User = user,
                Id = id
            };

            item.SetKey(key);
            item.SetSymbol(symbol);
            item.SetName(name);

            return item;
        }

        public void SetKey(string value)
        {
            this.Key = value;
        }


        public void SetName(string value)
        {
            this.Name = value;
        }

        public void SetSymbol(string value)
        {
            this.Symbol = value;
        }

    }
}