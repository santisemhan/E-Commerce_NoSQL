namespace Cart.Core.Repositories
{
    using Cart.Core.DataTransferObjects;
    using Cart.Core.Repositories.Contexts.Interfaces;
    using Cart.Core.Repositories.Interfaces;
    using Cassandra;
    using StackExchange.Redis;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserCartRepository : IUserCartRepository
    {
        private readonly IConnection<IDatabase> _redisConnection;
        private readonly IConnection<ISession> _cassandraConnection;

        public UserCartRepository(IConnection<IDatabase> redisConnection, IConnection<ISession> cassandraConnection)
        {
            _redisConnection = redisConnection;
            _cassandraConnection = cassandraConnection;
            _cassandraConnection.GetConnection().ChangeKeyspace("usercart");
        }

        public async Task ChangeUserCartAsync(UserCartDTO info)
        {
            var userId = info.User.UserId;

            await _redisConnection.GetConnection()
                .HashSetAsync($"user:{userId}", info.User.ToHashEntries());

            foreach (var product in info.Products)
            {
                var productKey = $"userCart:{userId}:{product.ProductCatalogId}";
                await _redisConnection.GetConnection()
                    .HashSetAsync(productKey, product.ToHashEntries());
            }

            await _redisConnection.GetConnection()
                .StringSetAsync($"userCart:{userId}", string.Join(",", info.Products.Select(p => p.ProductCatalogId.ToString())));

            //var query = await _cassandraConnection.GetConnection()
            //    .PrepareAsync("INSERT INTO UserCart (userId) VALUES (?)");

            //_cassandraConnection.GetConnection()
            //    .Execute(query.Bind(info.User.UserId));
        }

        public async Task<UserCartDTO?> GetUserCartAsync(Guid userId)
        {
            var result = new UserCartDTO();

            var userInfo = await _redisConnection.GetConnection()
                .HashGetAllAsync($"user:{userId}");

            if (userInfo.Length == 0)
               return null;

            result.AddUserData(userId, userInfo);

            var userProductsId = (await _redisConnection.GetConnection()
                .StringGetAsync($"userCart:{userId}")).ToString()
                .Split(",")
                .ToList();

            foreach (var productCartId in userProductsId)
            {
                var productInfo = await _redisConnection.GetConnection()
                    .HashGetAllAsync($"userCart:{userId}:{productCartId}");

                result.AddProductData(Guid.Parse(productCartId), productInfo);
            }
          
            return result;
        }
    }
}
