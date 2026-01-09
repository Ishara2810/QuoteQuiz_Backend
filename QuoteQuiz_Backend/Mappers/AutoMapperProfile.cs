using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.User;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Entities.Base;
using QuoteQuiz_Infrastructure.Data;

namespace QuoteQuiz_API.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Define your object-object mappings here

            CreateMap<AspNetUserEntity, ApplicationUser>();
            CreateMap<ApplicationUser, AspNetUserEntity>();

            CreateMap<AspNetUserEntity, UserDto>().ReverseMap();

            CreateMap<AspNetRoleEntity, IdentityRole>().ReverseMap();
            CreateMap<AspNetUserRoleEntity, IdentityUserRole<string>>().ReverseMap();

            CreateMap<QuoteEntity, Quote>().ReverseMap();
            CreateMap<QuoteEntity, QuoteDto>().ReverseMap();
            CreateMap<QuoteEntity, QuotePostDto>().ReverseMap();

        }
    }
}
