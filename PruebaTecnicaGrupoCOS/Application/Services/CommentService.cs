using AutoMapper;
using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Interfaces;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Core.Interfaces;
using PruebaTecnicaGrupoCOS.Helper;

namespace PruebaTecnicaGrupoCOS.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserAccountRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository,
                             IPostRepository postRepository,
                             IUserAccountRepository userRepository,
                             IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentResponse>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }

        public async Task<CommentResponse> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdWithDetailsAsync(id);
            if (comment == null) throw new KeyNotFoundException("Comment not found");
            return _mapper.Map<CommentResponse>(comment);
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsByPostIdAsync(int postId)
        {
            var comments = await _commentRepository.GetByPostIdWithDetailsAsync(postId);
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }

        public async Task<CommentResponse> CreateCommentAsync(CommentDto commentDto)
        {
            var user = await _userRepository.GetByIdAsync(commentDto.UserAccountId);
            if (user == null) throw new KeyNotFoundException("User not found");

            var post = await _postRepository.GetByIdAsync(commentDto.PostId);
            if (post == null) throw new KeyNotFoundException("Post not found");

            var comment = _mapper.Map<Comment>(commentDto);
            comment.CreatedAt = DateTime.UtcNow;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return await GetCommentByIdAsync(comment.Id);
        }

        public async Task<bool> UpdateCommentAsync(int id, CommentDto commentDto)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) throw new KeyNotFoundException("Comment not found");

            _mapper.Map(commentDto, comment);
            comment.UpdatedAt = DateTime.UtcNow;

            _commentRepository.Update(comment);
            return await _commentRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) throw new KeyNotFoundException("Comment not found");

            _commentRepository.Delete(comment);
            return await _commentRepository.SaveChangesAsync();
        }
    }
}
