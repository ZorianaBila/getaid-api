using GetAidBackend.Domain;

namespace GetAidBackend.Services.Dtos
{
    public class UserDto : DtoBase
    {
        public string Email { get; set; }
        public UserPrivateData PrivateData { get; set; }
    }
}