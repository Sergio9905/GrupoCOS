using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(string jwtToken, string refreshToken)> AuthenticateUser(string email, string password, string userIp);

        Task<string> RefreshJwtToken(string refreshToken, string userIp);
    }
}
