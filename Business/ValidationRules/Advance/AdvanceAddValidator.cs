using Business.Constants;
using Core.Extensions;
using Core.Utilities.Configuration;
using Entities.Dtos.Advance;
using Entities.Dtos.Validation;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Business.ValidationRules.Advance
{
    public class AdvanceAddValidator : AbstractValidator<AdvanceAddDto>
    {
        public AdvanceAddValidator()
        {
            AdvanceValidationValuesDto validationValues = ConfigurationHelper.Configuration.GetSection("ValidationValues:Advance").Get<AdvanceValidationValuesDto>();

            RuleFor(x => x.DocNumber).NotEmpty().WithMessage(Messages.CannotBeNullOrEmpty.Format("Sənəd nömrəsi"));
            RuleFor(x => x.DocNumber).MaximumLength(20).WithMessage(Messages.NotValid.Format("Sənəd nömrəsi"));
            RuleFor(x => x.DocNumber).Must(x => DocNumberPrexifIsValid(x, validationValues.DocNumberPrefixes!)).WithMessage(Messages.NotValid.Format("Sənəd nömrəsi"));

            RuleFor(x => x.Tin).NotEmpty().WithMessage(Messages.CannotBeNullOrEmpty.Format("VÖEN"));
            RuleFor(x => x.Tin).MaximumLength(15).WithMessage(Messages.NotValid.Format("VÖEN"));
            RuleFor(x => x.Tin).Matches("^(0|[1-9][0-9]*)$").WithMessage(Messages.NotValid.Format("VÖEN"));

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(Messages.MustBeGreaterThan.Format("Məbləğ", "sıfırdan"));

            RuleFor(x => x.File).NotEmpty().WithMessage(Messages.CannotBeNullOrEmpty.Format("Fayl"));
            RuleFor(x => x.File).Must(x => FileExtensionIsValid(Path.GetExtension(x?.FileName), validationValues.File!.Extensions!)).WithMessage(Messages.TypeNotValid.Format("Fayl"));
            RuleFor(x => x.File).Must(x => x?.Length <= validationValues.File!.MaxLength).WithMessage(Messages.BiggerThanMaxSize);
        }

        private bool DocNumberPrexifIsValid(string? arg, List<string> prefixes)
        {
            return !string.IsNullOrWhiteSpace(arg) && prefixes.Any(x => arg.StartsWith(x, StringComparison.OrdinalIgnoreCase));
        }

        private bool FileExtensionIsValid(string? arg, List<string> extensions)
        {
            return !string.IsNullOrWhiteSpace(arg) && extensions.Any(x => x.Equals(arg, StringComparison.OrdinalIgnoreCase));
        }
    }
}
