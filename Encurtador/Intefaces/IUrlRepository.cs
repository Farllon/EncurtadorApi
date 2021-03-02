using System.Threading.Tasks;
using Encurtador.Models;

namespace Encurtador.Intefaces
{
    public interface IUrlRepository
    {
        Task<Url> GetUrl(string hash);
        
        Task Create(Url url);
        
        Task<bool> Update(Url url);
        
        Task<bool> Delete(string hash);
    }
}