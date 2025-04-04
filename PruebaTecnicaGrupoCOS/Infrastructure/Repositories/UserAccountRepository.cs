using Microsoft.EntityFrameworkCore;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Core.Interfaces;
using PruebaTecnicaGrupoCOS.Infrastructure.Data;

namespace PruebaTecnicaGrupoCOS.Infrastructure.Repositories
{
    public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserAccount> GetByEmailAsync(string email)
            => await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<UserAccount> GetByUserNameAsync(string userName)
            => await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName == userName);

        public async Task<UserAccount> GetByRefreshTokenAsync(string refreshToken)
            => await _context.UserAccounts.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}
