using Core.Utilities.Pagination;
using Entities.Models;

namespace DataAccess.Abstract
{
    public interface ITransferDal
    {
        Task<Transfer?> GetByIdAsync(Transfer entity);
        Task<IReadOnlyCollection<Transfer>> GetAsync(Transfer? entity, PagingOptions? pagingOptions = null);
        Task<string> GetFile()
        Task<int> AddAsync(Transfer entity);
        Task<bool> UpdateAsync(Transfer entity);
        Task<bool> DeleteAsync(Transfer entity);
        Task<bool> ExistsAsync(Transfer? entity);
    }
}
