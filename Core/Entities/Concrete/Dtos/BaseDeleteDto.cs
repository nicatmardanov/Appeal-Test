using Core.Entities.Dtos;

namespace Core.Entities.Concrete.Dtos
{
    public class BaseDeleteDto<T> : BaseIdDto<T>
        where T : notnull
    {
    }
}
