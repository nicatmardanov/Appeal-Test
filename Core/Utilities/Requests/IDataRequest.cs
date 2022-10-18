using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Core.Utilities.Requests
{
    public interface IDataRequest<T> : IRequest where T : class, IDto
    {
        [FromQuery(Name = "params")]
        T? Parameters { get; set; }
    }
}
