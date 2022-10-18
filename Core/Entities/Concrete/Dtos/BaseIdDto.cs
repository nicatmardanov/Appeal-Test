using Microsoft.AspNetCore.Mvc;

namespace Core.Entities.Dtos
{
    public class BaseIdDto<T>
        where T : notnull
    {
        [FromHeader]
        public T Id { get; set; }
    }
}
