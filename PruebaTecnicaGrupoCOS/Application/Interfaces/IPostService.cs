using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponse>> GetAllPostsAsync();
        Task<PostResponse> GetPostByIdAsync(int id);
        Task<PostResponse> CreatePostAsync(PostDto postDto);
        Task<bool> UpdatePostAsync(int id, PostDto postDto);
        Task<bool> DeletePostAsync(int id);
    }
}
