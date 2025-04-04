using AutoMapper;
using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Interfaces;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Core.Interfaces;
using PruebaTecnicaGrupoCOS.Helper;

namespace PruebaTecnicaGrupoCOS.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserAccountRepository _userRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository,
                          IUserAccountRepository userRepository,
                          IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostResponse>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllWithUserAsync();
            return _mapper.Map<IEnumerable<PostResponse>>(posts);
        }

        public async Task<PostResponse> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdWithUserAsync(id);
            if (post == null) throw new KeyNotFoundException("Post not found");
            return _mapper.Map<PostResponse>(post);
        }

        public async Task<PostResponse> CreatePostAsync(PostDto postDto)
        {
            var user = await _userRepository.GetByIdAsync(postDto.UserAccountId);
            if (user == null) throw new KeyNotFoundException("User not found");

            var post = _mapper.Map<Post>(postDto);
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;

            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();

            return _mapper.Map<PostResponse>(post);
        }

        public async Task<bool> UpdatePostAsync(int id, PostDto postDto)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) throw new KeyNotFoundException("Post not found");

            _mapper.Map(postDto, post);
            post.UpdatedAt = DateTime.UtcNow;

            _postRepository.Update(post);
            return await _postRepository.SaveChangesAsync();
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) throw new KeyNotFoundException("Post not found");

            _postRepository.Delete(post);
            return await _postRepository.SaveChangesAsync();
        }
    }
}
