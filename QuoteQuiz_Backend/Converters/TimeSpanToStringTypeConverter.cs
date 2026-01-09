using AutoMapper;

namespace QuoteQuiz_API.Converters
{
    public class TimeSpanToStringTypeConverter : ITypeConverter<TimeSpan, string>
    {
        public string Convert(TimeSpan source, string destination, ResolutionContext context)
        {
            string formatedStr = source.ToString(@"hh\:mm");
            return formatedStr;

        }
    }
}
