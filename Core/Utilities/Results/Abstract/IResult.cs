namespace Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string? Message { get; }
        string? DetailedMessage { get; set; }
        int StatusCode { get; }
    }
}
