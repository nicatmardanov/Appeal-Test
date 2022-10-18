using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public class ValidationTool
    {
        public static void Validate(IValidator validator, object obj)
        {
            IValidationContext validationContext = new ValidationContext<object>(obj);
            var result = validator.Validate(validationContext);

            if (!result.IsValid)
            {
                List<string>? errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new Utilities.Exceptions.ValidationException(errors);
            }
        }
    }
}
