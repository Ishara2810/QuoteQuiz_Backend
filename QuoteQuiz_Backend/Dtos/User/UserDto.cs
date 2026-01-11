using QuoteQuiz_Domain.Enums;

namespace QuoteQuiz_API.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public QuizMode QuizMode { get; set; }
    }
}
