namespace GetAidBackend.Domain
{
    public class Order : EntityBase
    {
        public string UserId { get; set; }
        public List<NeedItem> NeedItems { get; set; }
        public Address Address { get; set; }
        public DateTime DateTime { get; set; } = new DateTime();

        public bool Collected { get; set; }
        public bool Delivered { get; set; }
    }
}