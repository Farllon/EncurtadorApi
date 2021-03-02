using Encurtador.Models;

namespace Encurtador.Intefaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}