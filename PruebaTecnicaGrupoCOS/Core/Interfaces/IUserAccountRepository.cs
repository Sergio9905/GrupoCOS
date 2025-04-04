using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS.Core.Interfaces
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        Task<UserAccount> GetByEmailAsync(string email);
        Task<UserAccount> GetByUserNameAsync(string userName);
        Task<UserAccount> GetByRefreshTokenAsync(string refreshToken);
    }
}
