using Microsoft.EntityFrameworkCore;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Core.Interfaces;
using PruebaTecnicaGrupoCOS.Infrastructure.Data;

namespace PruebaTecnicaGrupoCOS.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetAllWithDetailsAsync()
            => await _context.Comments
                .Include(c => c.UserAccount)
                .Include(c => c.Post)
                .ThenInclude(p => p.UserAccount)
                .ToListAsync();

        public async Task<Comment> GetByIdWithDetailsAsync(int id)
            => await _context.Comments
                .Include(c => c.UserAccount)
                .Include(c => c.Post)
                .ThenInclude(p => p.UserAccount)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Comment>> GetByPostIdWithDetailsAsync(int postId)
            => await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.UserAccount)
                .Include(c => c.Post)
                .ToListAsync();

        public async Task<IEnumerable<Comment>> GetByUserIdAsync(int userId)
            => await _context.Comments
                .Where(c => c.UserAccountId == userId)
                .Include(c => c.Post)
                .ToListAsync();
    }
}
