using GetAidBackend.Domain;

namespace GetAidBackend.Services.Dtos
{
    public class OrderDto : DtoBase
    {
        public string UserId { get; set; }
        public List<NeedItem> NeedItems { get; set; }
        public Address Address { get; set; }
        public DateTime DateTime { get; set; } = new DateTime();
        public bool Delivered { get; set; }
        public bool Collected { get; set; }
    }
}