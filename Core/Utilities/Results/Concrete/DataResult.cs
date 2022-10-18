using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }
        
        public DataResult(T data, int statusCode, string message) : base(statusCode, message)
        {
            Data = data;
        }

        public DataResult(T data, int statusCode) : base(statusCode)
        {
            Data = data;
        }
    }
}
