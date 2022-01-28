namespace Core.Utilities.Wrappers
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message, int statusCode = 200) : base(data, true, message, statusCode)
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(T data, int statusCode) : base(data, true, statusCode)
        {
        }

        public SuccessDataResult(string message, int statusCode = 200) : base(default, true, message, statusCode)
        {
        }

        public SuccessDataResult() : base(default, true)
        {
        }
    }



}
