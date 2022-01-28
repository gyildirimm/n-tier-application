namespace Core.Utilities.Wrappers
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message, int statusCode = 200) : base(true, message, statusCode)
        {
        }

        public SuccessResult() : base(true)
        {
        }

        public SuccessResult(int statusCode) : base(true, statusCode)
        {

        }
    }



}
