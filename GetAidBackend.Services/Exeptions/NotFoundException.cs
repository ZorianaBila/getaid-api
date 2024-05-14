namespace GetAidBackend.Services.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string itemName) : base($"{itemName ?? "Required item"} not found.")
        {
        }
    }
}