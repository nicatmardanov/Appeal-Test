using Entities.Models;

namespace DataAccess.Abstract
{
    public interface IAdvanceDal
    {
        Task<Guid> AddAsync(Advance entity);
    }
}
