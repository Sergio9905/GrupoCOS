using AutoMapper;
using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserAccount, UserAccountResponse>();
            CreateMap<UserAccount, UserAccountDto>();
            CreateMap<UserAccountDto, UserAccount>();

            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.UserAccount));
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.UserAccount))
                .ForMember(dest => dest.Post, opt => opt.MapFrom(src => src.Post));
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
        }
    }
}
