namespace QuoteQuiz_API.Dtos.Result
{
    public class Result<T>
    {
        public string Status { get; set; } = "Success";

        public T? Data { get; set; }

        public List<string>? ErrorMessages { get; set; }

        public List<string>? Errors { get; set; }

        public Result() { }

        public Result(string status, string errorMessage)
        {
            this.Status = status;
            this.ErrorMessages = new List<string> { errorMessage };
        }

        public Result(T? data)
        {
            this.Data = data;
        }

    }
}
