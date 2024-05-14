using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;

namespace GetAidBackend.Services.Abstractionas
{
    public interface IUserService : IServiceBase<User, UserDto>
    {
        Task<UserDto> GetByEmail(string email);

        Task<UserDto> UpdatePrivateData(string userId, UserPrivateData userPrivateData);
    }
}