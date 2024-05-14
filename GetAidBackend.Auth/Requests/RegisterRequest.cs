using GetAidBackend.Domain;

namespace GetAidBackend.Auth.Requests
{
    public class RegisterRequest
    {
        public UserPrivateData PrivateData { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}