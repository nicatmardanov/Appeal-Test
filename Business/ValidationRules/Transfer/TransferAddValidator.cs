using Business.Constants;
using Core.Extensions;
using Entities.Dtos.Transfer;
using FluentValidation;

namespace Business.ValidationRules.Transfer
{
    public class TransferAddValidator : AbstractValidator<TransferAddDto>
    {
        public TransferAddValidator()
        {
            RuleFor(x => x.AdvanceId).NotEmpty().WithMessage(Messages.NotValid.Format("Avansın ID-si"));

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(Messages.MustBeGreaterThan.Format("Məbləğ", "sıfırdan"));
        }
    }
}
