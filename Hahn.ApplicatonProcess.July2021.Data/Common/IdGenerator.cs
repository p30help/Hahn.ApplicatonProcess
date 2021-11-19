using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.July2021.Data.Common
{
    public interface IIdGenerator
    {
        Task<int> GenerateAsync();
    }

    public class IdGenerator : IIdGenerator
    {
        private int counter { get; set; } = 1;

        public async Task<int> GenerateAsync()
        {
            // must be get new id from sequence of sql server
            // but because our sql server is in-memory use static pattern
            counter++;

            return counter;
        }
    }
}
