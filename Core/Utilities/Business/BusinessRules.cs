using Core.Utilities.Invoke;
using Core.Utilities.Results.Abstract;
using System.Transactions;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public async Task<IResult?> Run(params IInvoke[] invokes)
        {
            foreach (IInvoke invoke in invokes)
            {
                object invokeResult = invoke.Run();
                IResult? result;

                if (invokeResult.GetType().BaseType?.Name == "Task" || invokeResult.GetType().BaseType?.BaseType?.Name == "Task")
                    result = await (Task<IResult>)invokeResult;
                else
                    result = (IResult)invokeResult;

                if (result?.Success != true)
                    return result;
            }

            return null;
        }

        public async Task RunSqlQueriesInTransaction(params IInvoke[] invokes)
        {
            TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                foreach (IInvoke invoke in invokes)
                {
                    object invokeResult = invoke.Run();
                    IResult? result;

                    if (invokeResult.GetType().BaseType?.Name == "Task" || invokeResult.GetType().BaseType?.BaseType?.Name == "Task")
                        result = await (Task<IResult>)invokeResult;
                    else
                        result = (IResult)invokeResult;

                    if (result?.Success != true)
                        throw new TransactionException(result?.Message);
                }

                transactionScope.Complete();
            }
            catch
            {
                throw;
            }
            finally
            {
                transactionScope.Dispose();
            }
        }
    }
}
