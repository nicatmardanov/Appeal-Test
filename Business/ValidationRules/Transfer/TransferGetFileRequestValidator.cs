using Business.Constants;
using Entities.Dtos.Transfer;
using FluentValidation;
using Core.Extensions;

namespace Business.ValidationRules.Transfer
{
    public class TransferGetFileRequestValidator : AbstractValidator<TransferGetFileRequestDto>
    {
        public TransferGetFileRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(Messages.MustBeGreaterThan.Format("Id", 0));
        }
    }
}
