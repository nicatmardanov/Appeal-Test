using Core.Entities;
using Core.Utilities.Requests;
using FluentValidation;

namespace Business.ValidationRules
{
    public class DataRequestValidator<T> : AbstractValidator<DataRequest<T>>
        where T : class, IDto
    {
        public DataRequestValidator()
        {
        }
    }
}
