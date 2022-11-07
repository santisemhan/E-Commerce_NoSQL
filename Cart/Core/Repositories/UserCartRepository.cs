namespace Cart.Core.Repositories
{
    using Cart.Core.DataTransferObjects;
    using Cart.Core.Repositories.Contexts.Interfaces;
    using Cart.Core.Repositories.Interfaces;
    using Cassandra;
    using StackExchange.Redis;
    using System;
    using System.Threading.Tasks;

    public class UserCartRepository : IUserCartRepository
    {
        private readonly IConnection<IConnectionMultiplexer> _redisConnection;
        private readonly IConnection<ISession> _cassandraConnection;

        public UserCartRepository(IConnection<IConnectionMultiplexer> redisConnection, IConnection<ISession> cassandraConnection)
        {
            _redisConnection = redisConnection;
            _cassandraConnection = cassandraConnection;
        }

        public async Task ChangeUserCartAsync(UserCartDTO info)
        {
            var key = new RedisKey(info.UserId);

            await _redisConnection.GetConnection()
                .GetDatabase()
                .HashSetAsync(key, info.ToHashEntries());

            var query = await _cassandraConnection.GetConnection()
                .PrepareAsync("INSERT INTO UserCart (userId) VALUES (?)");

            _cassandraConnection.GetConnection()
                .Execute(query.Bind(info.UserId));
        }

        public async Task<UserCartDTO> GetUserCartAsync(Guid userId)
        {
            var key = new RedisKey(userId.ToString());

            var userCart = await _redisConnection.GetConnection()
                .GetDatabase()
                .HashGetAllAsync(key);

            return new UserCartDTO(userCart);
        }
    }
}
