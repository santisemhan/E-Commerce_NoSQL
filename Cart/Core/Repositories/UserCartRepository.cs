namespace Cart.Core.Repositories
{
    using Cart.Core.DataTransferObjects;
    using Cart.Core.Repositories.Contexts.Interfaces;
    using Cart.Core.Repositories.Interfaces;
    using StackExchange.Redis;
    using System;
    using System.Threading.Tasks;

    public class UserCartRepository : IUserCartRepository
    {
        private readonly IConnection<IConnectionMultiplexer> _redisConnection;

        public UserCartRepository(IConnection<IConnectionMultiplexer> redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public async Task ChangeUserCartAsync(UserCartDTO info)
        {
            var key = new RedisKey(info.UserId);

            await _redisConnection.GetConnection().GetDatabase()
                .HashSetAsync(key, info.ToHashEntries());
        }

        public async Task<UserCartDTO> GetUserCartAsync(Guid userId)
        {
            var key = new RedisKey(userId.ToString());

            var userCart = await _redisConnection.GetConnection().GetDatabase()
                .HashGetAllAsync(key);

            return new UserCartDTO(userCart);
        }
    }
}
