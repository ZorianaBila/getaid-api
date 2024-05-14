using GetAidBackend.Domain;

namespace GetAidBackend.Auth.Entities

{
    public class RefreshTokenInfo : EntityBase
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}