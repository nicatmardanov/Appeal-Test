using Business.Constants;
using Core.Extensions;
using Entities.Dtos.Transfer;
using FluentValidation;

namespace Business.ValidationRules.Transfer
{
    public class TransferGetByIdRequestValidator : AbstractValidator<TransferGetByIdRequestDto>
    {
        public TransferGetByIdRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(Messages.MustBeGreaterThan.Format("Id", 0));
        }
    }
}
