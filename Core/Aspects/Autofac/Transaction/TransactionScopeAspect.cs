using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using TransactionScope transactionScope = new(TransactionScopeOption.RequiresNew);
            try
            {

                invocation.Proceed();
                var result = ((dynamic?)invocation.ReturnValue)?.Result;
                transactionScope.Complete();
            }
            catch
            {
                transactionScope.Dispose();
                throw;
            }
        }
    }
}
