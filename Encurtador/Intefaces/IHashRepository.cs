using System.Threading.Tasks;
using Encurtador.Models;

namespace Encurtador.Intefaces
{
    public interface IHashRepository
    {
        Task<Hash> GetHash(string hashCode);

        Task Create(Hash hash);

        Task<bool> Update(Hash hash);

        Task<bool> Delete(string hashCode);
    }
}