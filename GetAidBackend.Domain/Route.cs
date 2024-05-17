namespace GetAidBackend.Domain
{
    public class Route : EntityBase
    {
        public List<RouteItem> Items { get; set; }
        public Address StartPoint { get; set; }
        public int PathLength { get; set; }
    }

    public class RouteItem
    {
        public Address Address { get; set; }

        public string OrderId { get; set; }
    }
}