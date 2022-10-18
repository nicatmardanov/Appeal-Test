using Castle.DynamicProxy;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[]? SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            List<MethodInterceptionBaseAttribute>? classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methods = type.GetMethods()?.Where(x => x.Name == method.Name)?.ToList();

            for (int i = 0; i < methods?.Count; i++)
            {
                IEnumerable<MethodInterceptionBaseAttribute>? customMethodAttributes = methods[i].GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
                classAttributes?.AddRange(customMethodAttributes);
            }

            return classAttributes?.OrderBy(x => x.Priority)?.ToArray();
        }
    }
}
