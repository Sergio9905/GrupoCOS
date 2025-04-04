using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS.Core.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllWithDetailsAsync();
        Task<Comment> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Comment>> GetByPostIdWithDetailsAsync(int postId);
        Task<IEnumerable<Comment>> GetByUserIdAsync(int userId);
    }
}
