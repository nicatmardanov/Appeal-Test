using Entities.Models;

namespace DataAccess.Abstract
{
    public interface IAdvanceDal
    {
        Task<Advance> GetByIdAsync(Advance entity);
        Task<Guid> AddAsync(Advance entity);
        Task<bool> UpdateAmountAsync(Advance entity);
        Task<bool> ExistsAsync(Advance entity);
    }
}
