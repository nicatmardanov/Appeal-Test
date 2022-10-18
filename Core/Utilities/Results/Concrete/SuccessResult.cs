using Core.Utilities.Constants;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult(int statusCode, string message) : base(statusCode, message)
        {
        }

        public SuccessResult(int statusCode) : base(statusCode, Messages.Success)
        {
        }

        public SuccessResult(string message) : base(200, message)
        {
        }

        public SuccessResult() : base(200, Messages.Success)
        {
        }
    }
}
