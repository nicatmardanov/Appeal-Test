using Core.Utilities.Constants;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, int statusCode, string message) : base(data, statusCode, message)
        {
        }

        public ErrorDataResult(int statusCode, string message) : base(default, statusCode, message)
        {
        }

        public ErrorDataResult(T data, int statusCode) : base(data, statusCode, Messages.Fail)
        {
        }

        public ErrorDataResult(int statusCode) : base(default, statusCode, Messages.Fail)
        {
        }
        
        public ErrorDataResult(string message) : base(default, 400, message)
        {
        }

        public ErrorDataResult(T data) : base(data, 400, Messages.Fail)
        {
        }

        public ErrorDataResult() : base(default, 400)
        {
        }
    }
}
