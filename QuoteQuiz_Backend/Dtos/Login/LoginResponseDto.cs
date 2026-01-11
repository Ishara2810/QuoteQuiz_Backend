namespace QuoteQuiz_API.Dtos.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
