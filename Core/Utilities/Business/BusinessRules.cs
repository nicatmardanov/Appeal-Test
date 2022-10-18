using Core.Utilities.Invoke;
using Core.Utilities.Results.Abstract;

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
    }
}
