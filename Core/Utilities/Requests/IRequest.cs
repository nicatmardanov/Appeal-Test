using Core.Utilities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Core.Utilities.Requests
{
    public interface IRequest
    {
        [FromQuery(Name = "pagingOptions")]
        PagingOptions? PagingOptions { get; set; }
    }
}
