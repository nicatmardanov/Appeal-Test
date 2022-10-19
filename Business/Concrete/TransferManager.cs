using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.Transfer;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Invoke;
using Core.Utilities.Mapper;
using Core.Utilities.Requests;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos.Advance;
using Entities.Dtos.Transfer;
using Entities.Models;

namespace Business.Concrete
{
    public class TransferManager : ITransferService
    {
        private readonly ITransferDal _transferDal;
        private readonly IAdvanceService _advanceService;
        private readonly IMapper _mapper;

        public TransferManager(ITransferDal transferDal, IAdvanceService advanceService, IMapper mapper)
        {
            _transferDal = transferDal;
            _advanceService = advanceService;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(TransferGetByIdRequestValidator))]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<TransferGetDto>> GetByIdAsync(TransferGetByIdRequestDto requestDto)
        {
            Transfer request = new() { Id = requestDto.Id };
            Transfer? transfer = await _transferDal.GetByIdAsync(request);

            return transfer is not null
                ? new SuccessDataResult<TransferGetDto>(_mapper.Map<Transfer, TransferGetDto>(transfer))
                : new SuccessDataResult<TransferGetDto>();
        }

        [ValidationAspect(typeof(TransferGetFileRequestValidator))]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<string?>> GetFilePathAsync(TransferGetFileRequestDto requestDto)
        {
            Transfer request = new() { Id = requestDto.Id };
            string? result = await _transferDal.GetFilePathAsync(request);

            return !string.IsNullOrEmpty(result)
                ? new SuccessDataResult<string>(data: result)
                : new SuccessDataResult<string>();
        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<List<TransferGetDto>>> GetAsync(DataRequest<TransferGetListRequestDto> dataRequestDto)
        {
            List<Transfer> transferList = await _transferDal.GetAsync(dataRequestDto);

            return transferList.Any()
                ? new SuccessDataResult<List<TransferGetDto>>(_mapper.Map<List<Transfer>, List<TransferGetDto>>(transferList))
                : new SuccessDataResult<List<TransferGetDto>>();
        }

        [ValidationAspect(typeof(TransferAddValidator))]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<decimal>> AddAsync(TransferAddDto addDto)
        {
            BusinessRules bs = new();
            IDataResult<AdvanceGetDto> advanceDataResult = await _advanceService.GetByIdAsync(new() { Id = addDto.AdvanceId });
            IResult? validationIssue = await bs.Run(CheckIfAdvanceDataExists(advanceDataResult), CheckIfAmountIsValid(advanceDataResult.Data?.Amount, addDto.Amount));
            if (validationIssue is not null)
                return new ErrorDataResult<decimal>(validationIssue.Message);

            Transfer? transfer = _mapper.Map<TransferAddDto, Transfer>(addDto);

            await bs.RunSqlQueriesInTransaction(
                new Invoke<Func<Task<IResult>>>(async () =>
                {
                    return await _transferDal.AddAsync(transfer) > 0 ? new SuccessResult() : new ErrorResult();
                }),
                new Invoke<Func<Task<IResult>>>(async () =>
                {
                    return await _advanceService.UpdateAmountAsync(new() { Id = transfer.AdvanceId, Amount = -transfer.Amount });
                }));

            return new SuccessDataResult<decimal>(data: advanceDataResult.Data!.Amount - transfer.Amount);

            //using TransactionScope ts = new();
            //try
            //{
            //    _ = await _transferDal.AddAsync(transfer);
            //    IResult updateAmountResult = await _advanceService.UpdateAmountAsync(new() { Id = transfer.AdvanceId, Amount = -transfer.Amount });
            //    ts.Complete();
            //    return updateAmountResult.Success ? new SuccessDataResult<decimal>(data: advanceDataResult.Data!.Amount - transfer.Amount) : new ErrorDataResult<decimal>(updateAmountResult.Message!);
            //}
            //catch
            //{
            //    ts.Dispose();
            //    throw;
            //}
        }

        [ValidationAspect(typeof(TransferUpdateValidator))]
        [LogAspect(typeof(FileLogger))]
        public async Task<IResult> UpdateAsync(TransferUpdateDto updateDto)
        {
            Transfer request = _mapper.Map<TransferUpdateDto, Transfer>(updateDto);
            Transfer? transfer = await _transferDal.GetByIdAsync(request);
            BusinessRules bs = new();

            IResult? validationIssue = await bs.Run(CheckIfTransferIsNotNull(transfer), CheckIfAdvanceIsValid(transfer?.AdvanceId, updateDto.Amount));
            if (validationIssue is not null)
                return new ErrorResult(validationIssue.Message!);

            if (transfer!.Amount == request.Amount)
                return new SuccessResult();

            await bs.RunSqlQueriesInTransaction(
                new Invoke<Func<Task<IResult>>>(async () =>
                {
                    return await _transferDal.UpdateAsync(request) ? new SuccessResult() : new ErrorResult();
                }),
                new Invoke<Func<Task<IResult>>>(async () =>
                {
                    return await _advanceService.UpdateAmountAsync(new() { Id = transfer.AdvanceId, Amount = transfer.Amount - request.Amount });
                }));

            return new SuccessResult();
        }

        [ValidationAspect(typeof(TransferDeleteValidator))]
        [LogAspect(typeof(FileLogger))]
        public async Task<IResult> DeleteAsync(TransferDeleteDto deleteDto)
        {
            Transfer? request = _mapper.Map<TransferDeleteDto, Transfer>(deleteDto);
            Transfer? transfer = await _transferDal.GetByIdAsync(request);
            BusinessRules bs = new();

            IResult? validationIssue = await bs.Run(CheckIfTransferIsNotNull(transfer));
            if (validationIssue is not null)
                return new ErrorResult(validationIssue.Message!);


            await bs.RunSqlQueriesInTransaction(
                new Invoke<Func<Task<IResult>>>(async () =>
                {
                    return await _transferDal.DeleteAsync(request) ? new SuccessResult() : new ErrorResult();
                }),
                new Invoke<Func<Task<IResult>>>(async () =>
                {
                    return await _advanceService.UpdateAmountAsync(new() { Id = transfer!.AdvanceId, Amount = transfer.Amount });
                }));

            return new SuccessResult();

            //using TransactionScope ts = new();
            //_ = await _transferDal.DeleteAsync(request);
            //IResult result = await _advanceService.UpdateAmountAsync(new() { Id = transfer!.AdvanceId, Amount = transfer.Amount });
            //ts.Complete();

            //return result;
        }


        #region Private methods
        private Invoke<Func<IResult>> CheckIfTransferIsNotNull(Transfer? transfer)
            => new(() => transfer is null ? new ErrorResult(Messages.NotFound.Format("Transfer")) : new SuccessResult());

        private Invoke<Func<Task<IResult>>> CheckIfAdvanceIsValid(Guid? advanceId, decimal transferAmount)
        {
            return new(async () =>
            {
                IDataResult<AdvanceGetDto> advanceDataResult = await _advanceService.GetByIdAsync(new() { Id = (Guid)advanceId! });
                IResult? validationIssues
                                    = await new BusinessRules()
                                    .Run(CheckIfAdvanceDataExists(advanceDataResult),
                                         CheckIfAmountIsValid(advanceDataResult.Data?.Amount, transferAmount));

                return validationIssues is not null ? new ErrorResult(validationIssues.Message!) : new SuccessResult();
            });
        }
        private Invoke<Func<IResult>> CheckIfAdvanceDataExists(IDataResult<AdvanceGetDto> advanceDataResult)
        {
            return new(() => advanceDataResult.Data is not null ? new SuccessResult() : new ErrorResult(Messages.NotFound.Format("Avans")));
        }

        private Invoke<Func<IResult>> CheckIfAmountIsValid(decimal? availableAmount, decimal transferAmount)
        {
            return new(() => availableAmount >= transferAmount ? new SuccessResult() : new ErrorResult(Messages.MustBeGreaterThan.Format("Transfer məbləği", "mümkün avans məbləğindən")));
        }
        #endregion
    }
}
