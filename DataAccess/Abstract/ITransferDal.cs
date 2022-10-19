using Core.Utilities.Requests;
using Entities.Dtos.Transfer;
using Entities.Models;

namespace DataAccess.Abstract
{
    public interface ITransferDal
    {
        Task<Transfer?> GetByIdAsync(Transfer entity);
        Task<List<Transfer>> GetAsync(DataRequest<TransferGetListRequestDto> requestDto);
        Task<string?> GetFilePathAsync(Transfer entity);
        Task<int> AddAsync(Transfer entity);
        Task<bool> UpdateAsync(Transfer entity);
        Task<bool> DeleteAsync(Transfer entity);
    }
}
