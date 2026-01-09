namespace QuoteQuiz_API.Dtos.Quote
{
    public class QuoteDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}
