namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string? MethodName { get; set; }
        public string? ConnectionInfo { get; set; }
        public string? Text { get; set; }
        public string? DetailedText { get; set; }
        public Dictionary<string, string>? HeaderData { get; set; }
        public List<LogParameter>? LogParameters { get; set; }
    }


}
