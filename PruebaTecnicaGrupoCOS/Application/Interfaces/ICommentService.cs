using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponse>> GetAllCommentsAsync();
        Task<CommentResponse> GetCommentByIdAsync(int id);
        Task<CommentResponse> CreateCommentAsync(CommentDto commentDto);
        Task<bool> UpdateCommentAsync(int id, CommentDto commentDto);
        Task<bool> DeleteCommentAsync(int id);
        Task<IEnumerable<CommentResponse>> GetCommentsByPostIdAsync(int postId);
    }
}
