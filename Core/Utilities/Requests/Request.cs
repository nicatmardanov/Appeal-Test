using Core.Utilities.Pagination;

namespace Core.Utilities.Requests
{
    public class Request : IRequest
    {
        public PagingOptions? PagingOptions { get; set; }
    }
}
