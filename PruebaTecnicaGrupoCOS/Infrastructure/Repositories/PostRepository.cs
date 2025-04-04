using Microsoft.EntityFrameworkCore;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Core.Interfaces;
using PruebaTecnicaGrupoCOS.Infrastructure.Data;

namespace PruebaTecnicaGrupoCOS.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Post>> GetAllWithUserAsync()
            => await _context.Posts
                .Include(p => p.UserAccount)
                .ToListAsync();

        public async Task<Post> GetByIdWithUserAsync(int id)
            => await _context.Posts
                .Include(p => p.UserAccount)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
            => await _context.Posts
                .Where(p => p.UserAccountId == userId)
                .Include(p => p.UserAccount)
                .ToListAsync();
    }
}
