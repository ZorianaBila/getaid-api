namespace GetAidBackend.Domain
{
    public class Order : EntityBase
    {
        public string UserId { get; set; }
        public List<NeedItem> NeedItems { get; set; }
        public Address Address { get; set; }

        public bool Collected { get; set; }
        public bool Delivered { get; set; }
    }
}