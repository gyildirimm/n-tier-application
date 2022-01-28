namespace Core.Utilities.Wrappers
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message,int statusCode = 400) : base(data, false, message, statusCode)
        {
        }

        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(string message, int statusCode = 400) : base(default, false, message, statusCode)
        {
        }

        public ErrorDataResult() : base(default, false)
        {
        }
    }



}
