using Core.Utilities.Invoke;

namespace Core.Utilities.Invoke
{
    public class Invoke<T> : IInvoke where T : Delegate
    {
        public readonly object[] Parameters;
        public T Delegate { get; set; }


        public Invoke(T d, params object[] parameters)
        {
            Delegate = d;
            Parameters = parameters;
        }

        public object Run()
        {
            return Delegate.DynamicInvoke(Parameters)!;
        }
    }
}
