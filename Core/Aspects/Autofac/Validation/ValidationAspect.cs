using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Constants;
using Core.Utilities.Interceptors;
using Castle.DynamicProxy;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            IValidator? validator = (IValidator)Activator.CreateInstance(_validatorType)!;
            Type? entityType = _validatorType.BaseType?.GetGenericArguments()[0];
            IEnumerable<object>? entites = invocation.Arguments?.Where(x => x.GetType() == entityType);

            foreach (object? entity in entites)
            {
                ValidationTool.Validate(validator, entity);
            }

        }
    }
}
