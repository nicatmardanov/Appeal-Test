using Business.Constants;
using Core.Extensions;
using Entities.Dtos.Advance;
using FluentValidation;

namespace Business.ValidationRules.Advance
{
    public class AdvanceGetByIdRequestValidator : AbstractValidator<AdvanceGetByIdRequestDto>
    {
        public AdvanceGetByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(Messages.NotValid.Format("Id"));
        }
    }
}
