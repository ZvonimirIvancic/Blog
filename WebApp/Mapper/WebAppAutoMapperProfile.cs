using AutoMapper;
using DAL.Models;
using WebApp.ViewModels;

namespace WebApp.Mapper
{
    public class WebAppAutoMapperProfile : Profile
    {
        public WebAppAutoMapperProfile()
        {
            CreateMap<User, VMUser>();
            CreateMap<VMUser, User>();
            CreateMap<Comment, VMComment>();
            CreateMap<VMComment, Comment>();
            CreateMap<BlogPost, VMBlogPost>();
            CreateMap<VMBlogPost, BlogPost>();
        }
    }
}
