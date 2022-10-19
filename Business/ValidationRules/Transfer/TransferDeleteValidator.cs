using Business.Constants;
using FluentValidation;
using Core.Extensions;
using Entities.Dtos.Transfer;

namespace Business.ValidationRules.Transfer
{
    public class TransferDeleteValidator : AbstractValidator<TransferDeleteDto>
    {
        public TransferDeleteValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(Messages.MustBeGreaterThan.Format("Id", "sıfırdan"));
        }
    }
}
