using GetAidBackend.Domain;

namespace GetAidBackend.Services.Dtos
{
    public class RouteDto : DtoBase
    {
        public List<RouteItem> Items { get; set; }
        public Address StartPoint { get; set; }
        public DateTime DateTime { get; set; }
        public int PathLength { get; set; }
    }
}