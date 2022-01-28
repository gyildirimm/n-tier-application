namespace Core.Utilities.Wrappers
{
    public class Result : IResult
    {
        public bool IsSuccess { get; }

        public string Message { get; }

        public int StatusCode { get; }

        public Result(bool success, string message, int statusCode) : this (success)
        {
            Message = message;
            StatusCode = statusCode; 
        }

        public Result(bool success, int statusCode) : this(success)
        {
            StatusCode = statusCode;
        }

        public Result(bool success)
        {
            IsSuccess = success;
        }

    }



}
