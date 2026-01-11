namespace QuoteQuiz_API.Dtos.Login
{
    public class RefreshTokenRequestDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
