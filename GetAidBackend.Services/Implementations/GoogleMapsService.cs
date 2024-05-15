using GetAidBackend.Services.Abstractionas;
using Newtonsoft.Json.Linq;

namespace GetAidBackend.Services.Implementations
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private const string _baseUrl = "https://maps.googleapis.com/maps/api";
        private readonly string apiKey;

        public GoogleMapsService()
        {
            apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");
        }

        public async Task<int[,]> GetDistanceMatrix(string[] addresses)
        {
            int[,] distanceMatrix = new int[addresses.Length, addresses.Length];

            for (int i = 0; i < addresses.Length; i++)
            {
                for (int j = i + 1; j < addresses.Length; j++)
                {
                    string url = $"{_baseUrl}/distancematrix/json?" +
                        $"origins={Uri.EscapeDataString(addresses[i])}&" +
                        $"destinations={Uri.EscapeDataString(addresses[j])}&" +
                        $"key={apiKey}";

                    using HttpClient client = new HttpClient();
                    string json = await client.GetStringAsync(url);

                    JObject response = JObject.Parse(json);
                    JToken element = response["rows"][0]["elements"][0];

                    distanceMatrix[i, j] = (int)element["distance"]["value"];
                    distanceMatrix[j, i] = (int)element["distance"]["value"];
                    //Console.WriteLine($"Duration: {element["duration"]["value"]}");
                }
            }

            return distanceMatrix;
        }
    }
}