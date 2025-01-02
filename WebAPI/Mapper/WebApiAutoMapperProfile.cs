using AutoMapper;
using DAL.Models;
using Humanizer.Localisation;
using WebAPI.Dtos;

namespace WebAPI.Mapper
{
    public class WebApiAutoMapperProfile : Profile
    {
        public WebApiAutoMapperProfile()
        {
            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<BlogPostDto, BlogPost>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
