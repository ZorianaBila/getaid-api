using AutoMapper;
using GetAidBackend.Domain;
using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.Dtos;
using GetAidBackend.Services.Exceptions;
using GetAidBackend.Storage.Abstractions;
using MongoDB.Driver;

namespace GetAidBackend.Services.Implementations
{
    public class UserService : ServiceBase<User, UserDto, IUserRepository>, IUserService
    {
        private readonly IMongoClient _client;
        private readonly IOrderService _orderService;

        public UserService(
            IMongoClient client,
            IOrderService orderService,
            IUserRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _orderService = orderService;
            _client = client;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await _repository.GetByEmail(email);
            if (user == null)
            {
                throw new NotFoundException($"The user '{email}'");
            }
            return GetDtoFromEntity(user);
        }

        public async Task<UserDto> UpdatePrivateData(string userId, UserPrivateData userPrivateData)
        {
            var user = await _repository.UpdatePrivateData(userId, userPrivateData);
            if (user == null)
            {
                throw new NotFoundException($"The user");
            }
            return GetDtoFromEntity(user);
        }

        public override async Task DeleteById(string userId)
        {
            using var session = await _client.StartSessionAsync();
            session.StartTransaction();
            try
            {
                await _orderService.DeleteUserOrders(userId);
                await _repository.DeleteById(userId);
            }
            catch (Exception e)
            {
                await session.AbortTransactionAsync();
                throw new MongoTransactionException(e.Message);
            }
        }
    }
}