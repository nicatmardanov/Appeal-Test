using Business.Constants;
using Core.Extensions;
using Entities.Dtos.Advance;
using FluentValidation;

namespace Business.ValidationRules.Advance
{
    public class AdvanceAddValidator : AbstractValidator<AdvanceAddDto>
    {
        public AdvanceAddValidator()
        {
            RuleFor(x => x.DocNumber).NotEmpty().WithMessage(Messages.CannotBeNullOrEmpty.Format("Sənəd nömrəsi"));
            RuleFor(x => x.DocNumber).MaximumLength(20).WithMessage(Messages.NotValid.Format("Sənəd nömrəsi"));
            RuleFor(x => x.DocNumber).Must(x => x?.StartsWith("TXD") == true).WithMessage(Messages.NotValid.Format("Sənəd nömrəsi"));

            RuleFor(x => x.Tin).NotEmpty().WithMessage(Messages.CannotBeNullOrEmpty.Format("VÖEN"));
            RuleFor(x => x.Tin).MaximumLength(15).WithMessage(Messages.NotValid.Format("VÖEN"));
            RuleFor(x => x.Tin).Matches("^(0|[1-9][0-9]*)$").WithMessage(Messages.NotValid.Format("VÖEN"));

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(Messages.MustBeGreaterThanZero.Format("Məbləğ"));
        }
    }
}
