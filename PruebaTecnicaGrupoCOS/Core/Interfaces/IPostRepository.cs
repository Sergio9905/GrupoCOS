using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllWithUserAsync();
        Task<Post> GetByIdWithUserAsync(int id);
        Task<IEnumerable<Post>> GetByUserIdAsync(int userId);
    }
}
