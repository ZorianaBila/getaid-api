namespace GetAidBackend.Services.Abstractionas
{
    public interface IGoogleMapsService
    {
        Task<int[,]> GetDistanceMatrix(string[] addresses);
    }
}