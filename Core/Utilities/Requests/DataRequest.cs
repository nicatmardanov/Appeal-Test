using Core.Entities;

namespace Core.Utilities.Requests
{
    public class DataRequest<T> : Request, IDataRequest<T> where T : class, IDto
    {
        public T? Parameters { get; set; } = null;
    }
}