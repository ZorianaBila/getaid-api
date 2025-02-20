﻿using AutoMapper;
using GetAidBackend.Domain;
using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.Dtos;
using GetAidBackend.Storage.Abstractions;
using MongoDB.Driver;

namespace GetAidBackend.Services.Implementations
{
    public class OrderService : ServiceBase<Order, OrderDto, IOrderRepository>, IOrderService
    {
        private readonly IMongoClient _client;

        public OrderService(
            IMongoClient client,
            IOrderRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _client = client;
        }

        public async Task<OrderDto> Add(string userId, Order order)
        {
            order.UserId = userId;
            var addedOrder = await _repository.Add(order);
            return GetDtoFromEntity(addedOrder);
        }

        public async Task<List<OrderDto>> GetUserOrders(string userId)
        {
            var orders = await _repository.GetUserOrders(userId);

            return GetDtosFromEntities(orders);
        }

        public async Task DeleteUserOrders(string userId)
        {
            await _repository.DeleteUserOrders(userId);
        }

        public async Task<List<OrderDto>> GetNonDeliveredOrders()
        {
            var results = await _repository.GetAll(_ => _.Delivered == false);
            return GetDtosFromEntities(results);
        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var results = await _repository.GetAll();
            return GetDtosFromEntities(results);
        }

        public async Task<List<OrderDto>> GetNonDeliveredCollectedOrders()
        {
            var results = await _repository.GetAll(_ => _.Delivered == false && _.Collected == true);
            return GetDtosFromEntities(results);
        }

        public async Task<List<OrderDto>> GetNonCollectedOrders()
        {
            var results = await _repository.GetAll(_ => _.Collected == false);
            return GetDtosFromEntities(results);
        }

        public async Task<List<OrderDto>> SearchById(string id)
        {
            var results = await _repository.GetAll(_ => _.Id.StartsWith(id));
            return GetDtosFromEntities(results);
        }

        public async Task CollectOrder(string orderId)
        {
            await _repository.CollectOrder(orderId);
        }
    }
}