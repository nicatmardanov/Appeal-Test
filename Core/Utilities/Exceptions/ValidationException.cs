namespace Core.Utilities.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(List<string> messages) : base(string.Join(",", messages))
        {
        }
    }
}
