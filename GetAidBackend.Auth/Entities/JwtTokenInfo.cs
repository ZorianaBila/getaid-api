namespace GetAidBackend.Auth.Entities
{
    public class JwtTokenInfo
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiresInMinutes { get; set; }
        public int RefreshTokenExpiresInMinutes { get; set; }
    }
}