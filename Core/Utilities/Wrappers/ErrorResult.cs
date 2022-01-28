namespace Core.Utilities.Wrappers
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message, int statusCode = 400) : base(false, message, statusCode)
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }



}
