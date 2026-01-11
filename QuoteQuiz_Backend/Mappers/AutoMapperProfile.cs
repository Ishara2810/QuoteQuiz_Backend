using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuoteQuiz_API.Dtos.Quiz;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.Role;
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

            CreateMap<AspNetUserRoleEntity, IdentityUserRole<string>>().ReverseMap();

            CreateMap<QuoteEntity, Quote>().ReverseMap();
            CreateMap<QuoteEntity, QuoteDto>().ReverseMap();
            CreateMap<QuoteEntity, QuotePostDto>().ReverseMap();

            CreateMap<AspNetRoleEntity, IdentityRole>().ReverseMap();
            CreateMap<AspNetRoleEntity, RoleDto>().ReverseMap();

            CreateMap<QuizQuestionEntity, QuizQuestionDto>().ReverseMap();

            CreateMap<QuizAttemptEntity, QuizAttempt>().ReverseMap();
            CreateMap<QuizAttemptEntity, QuizAttemptDto>().ReverseMap();

            CreateMap<QuizAnswerSubmitEntity, SubmitAnswerDto>().ReverseMap();

            

        }
    }
}
