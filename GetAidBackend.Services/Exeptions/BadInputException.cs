namespace GetAidBackend.Services.Exceptions
{
    public class BadInputException : BaseException
    {
        public BadInputException() : base("Bad input")
        {
        }

        public BadInputException(string message) : base(message)
        {
        }
    }
}