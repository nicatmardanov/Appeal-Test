using Core.Utilities.Requests;
using Core.Utilities.Results.Abstract;
using Entities.Dtos.Transfer;

namespace Business.Abstract
{
    public interface ITransferService
    {
        Task<IDataResult<TransferGetDto>> GetByIdAsync(TransferGetByIdRequestDto requestDto);
        Task<IDataResult<string?>> GetFilePathAsync(TransferGetFileRequestDto requestDto);
        Task<IDataResult<List<TransferGetDto>>> GetAsync(DataRequest<TransferGetListRequestDto> dataRequestDto);
        Task<IDataResult<decimal>> AddAsync(TransferAddDto addDto);
        Task<IResult> UpdateAsync(TransferUpdateDto updateDto);
        Task<IResult> DeleteAsync(TransferDeleteDto deleteDto);
    }
}
