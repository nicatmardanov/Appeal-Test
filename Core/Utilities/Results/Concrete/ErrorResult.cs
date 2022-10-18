using Core.Utilities.Constants;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult(int statusCode, string message) : base(statusCode, message)
        {
        }

        public ErrorResult(int statusCode) : base(statusCode, Messages.Fail)
        {
        }

        public ErrorResult(string message) : base(400, message)
        {
        }

        public ErrorResult() : base(400, Messages.Fail)
        {
        }
    }
}
