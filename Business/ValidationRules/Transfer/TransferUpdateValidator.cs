using Business.Constants;
using FluentValidation;
using Core.Extensions;
using Entities.Dtos.Transfer;

namespace Business.ValidationRules.Transfer
{
    public class TransferUpdateValidator : AbstractValidator<TransferUpdateDto>
    {
        public TransferUpdateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(Messages.MustBeGreaterThanZero.Format("Id"));

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(Messages.MustBeGreaterThanZero.Format("Məbləğ"));
        }
    }
}
