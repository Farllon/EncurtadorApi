using System;
using System.Threading.Tasks;
using Encurtador.Models;

namespace Encurtador.Intefaces
{
    public interface IUserRepository
    {
        Task Create(User user);

        Task<bool> Delete(Guid id);

        Task<User> GetUser(Guid id);

        Task<User> GetByCred(string user, string password);

        Task<bool> Update(User user);
    }
}