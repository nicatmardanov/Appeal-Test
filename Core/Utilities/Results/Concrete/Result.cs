using Core.Utilities.Results.Abstract;
using System.Text.Json.Serialization;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string? Message { get; }
        [JsonIgnore]
        public string? DetailedMessage { get; set; }
        public int StatusCode { get; }


        public Result(int statusCode, string message) : this(statusCode)
        {
            Message = message;
        }

        public Result(int statusCode)
        {
            StatusCode = statusCode;
            Success = statusCode >= 200 && statusCode <= 299;
        }
    }
}
