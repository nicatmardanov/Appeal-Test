using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.Advance;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.File;
using Core.Utilities.Invoke;
using Core.Utilities.Mapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos.Advance;
using Entities.Models;

namespace Business.Concrete
{
    public class AdvanceManager : IAdvanceService
    {
        private readonly IAdvanceDal _advanceDal;
        private readonly IMapper _mapper;

        public AdvanceManager(IAdvanceDal advanceDal, IMapper mapper)
        {
            _advanceDal = advanceDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(AdvanceGetByIdRequestValidator))]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<AdvanceGetDto>> GetByIdAsync(AdvanceGetByIdRequestDto requestDto)
        {
            Advance? advance = await _advanceDal.GetByIdAsync(new() { Id = requestDto.Id });

            return advance is not null
                ? new SuccessDataResult<AdvanceGetDto>(_mapper.Map<Advance, AdvanceGetDto>(advance))
                : new SuccessDataResult<AdvanceGetDto>();
        }

        [ValidationAspect(typeof(AdvanceAddValidator))]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<Guid>> AddAsync(AdvanceAddDto addDto)
        {
            Advance request = _mapper.Map<AdvanceAddDto, Advance>(addDto);

            var validationIssue = await new BusinessRules().Run(CheckIfDocNumberNotExists(request));
            if (validationIssue is not null)
                return new ErrorDataResult<Guid>(validationIssue.Message);

            request.FilePath = await FileHelper.SaveAsync(addDto.File!);
            return new SuccessDataResult<Guid>(await _advanceDal.AddAsync(request));
        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IResult> UpdateAmountAsync(AdvanceUpdateAmountDto updateAmountDto)
        {
            Advance request = new() { Id = updateAmountDto.Id, Amount = updateAmountDto.Amount };
            return await _advanceDal.UpdateAmountAsync(request) ? new SuccessResult() : new ErrorResult();
        }

        #region Private methods
        private Invoke<Func<Task<IResult>>> CheckIfDocNumberNotExists(Advance advance)
        {
            return new(async () =>
            {
                return !await _advanceDal.ExistsAsync(advance) ? new SuccessResult() : new ErrorResult(Messages.DataAlreadyExists.Format("Sənəd nömrəsi"));
            });
        }
        #endregion
    }
}
