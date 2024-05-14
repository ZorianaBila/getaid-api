namespace GetAidBackend.Services.Exceptions
{
    public class MongoTransactionException : BaseException
    {
        public MongoTransactionException(string error)
            : base($"Error in MongoDB transaction: {error}")

        {
        }
    }
}