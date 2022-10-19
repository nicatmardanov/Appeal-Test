using Core.Utilities.Results.Abstract;
using Entities.Dtos.Advance;

namespace Business.Abstract
{
    public interface IAdvanceService
    {
        Task<IDataResult<AdvanceGetDto>> GetByIdAsync(AdvanceGetByIdRequestDto requestDto);
        Task<IDataResult<Guid>> AddAsync(AdvanceAddDto addDto);
        Task<IResult> UpdateAmountAsync(AdvanceUpdateAmountDto updateAmountDto);
    }
}
