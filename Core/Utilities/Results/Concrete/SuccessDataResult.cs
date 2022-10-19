using Core.Utilities.Constants;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, int statusCode, string message) : base(data, statusCode, message)
        {
        }

        public SuccessDataResult(int statusCode, string message) : base(default, statusCode, message)
        {
        }

        public SuccessDataResult(T data, int statusCode) : base(data, statusCode, Messages.Success)
        {
        }

        public SuccessDataResult(T data, string message) : base(data, 200, Messages.Success)
        {
        }

        public SuccessDataResult(int statusCode) : base(default, statusCode, Messages.Success)
        {
        }

        public SuccessDataResult(string message) : base(default, 200, message)
        {
        }

        public SuccessDataResult(T data) : base(data, 200, Messages.Success)
        {
        }

        public SuccessDataResult() : base(default, 200, Messages.NotFound)
        {
        }
    }
}
