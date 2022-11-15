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
        }

        public async Task ChangeUserCartAsync(UserCartDTO info)
        {
            var processId = Guid.NewGuid();
            var userId = info.User.UserId;

            await _redisConnection.GetConnection()
                .HashSetAsync($"user:{userId}", info.User.ToHashEntries());

            var userProductsId = await _redisConnection.GetConnection()
                .SetMembersAsync($"userCart:{userId}");

            foreach(var product in userProductsId.Where(p => !info.Products.Any(pr => pr.ProductCatalogId.ToString() == p.ToString())))
            {
                await _redisConnection.GetConnection()
                    .SetRemoveAsync($"userCart:{userId}", product);
            }

            foreach (var product in info.Products)
            {
                var productKey = $"userCart:{userId}:{product.ProductCatalogId}";
                await _redisConnection.GetConnection()
                    .HashSetAsync(productKey, product.ToHashEntries());

                var query = await _cassandraConnection.GetConnection()
                    .PrepareAsync(@"INSERT INTO cart (id, moment, userId, imageurl, price, productcatalogid, productname, quantity) 
                                    VALUES (?,?,?,?,?,?,?,?)");

                _cassandraConnection.GetConnection()
                    .Execute(query.Bind(processId, DateTime.Now, info.User.UserId, product.ImageURL, product.Price, 
                        product.ProductCatalogId, product.ProductName, product.Quantity));

                await _redisConnection.GetConnection()
                .SetAddAsync($"userCart:{userId}", new RedisValue(product.ProductCatalogId.ToString()));
            }
        }

        public async Task<UserCartDTO?> GetUserCartAsync(Guid userId)
        {
            var result = new UserCartDTO();

            var userInfo = await _redisConnection.GetConnection()
                .HashGetAllAsync($"user:{userId}");

            if (userInfo.Length == 0)
               return null;

            result.AddUserData(userId, userInfo);

            var userProductsId = await _redisConnection.GetConnection()
                .SetMembersAsync($"userCart:{userId}");

            foreach (var productCartId in userProductsId)
            {
                var productInfo = await _redisConnection.GetConnection()
                    .HashGetAllAsync($"userCart:{userId}:{productCartId}");

                result.AddProductData(Guid.Parse(productCartId.ToString()), productInfo);
            }
          
            return result;
        }

        public async Task RestoreCart(Guid userId, Guid logId)
        {
            var processId = Guid.NewGuid();
            var productsCatalogId = new List<string>();

            var query = _cassandraConnection.GetConnection()
                .Execute(@$"SELECT * FROM cart
                            WHERE id = {logId}");

            foreach (var row in query)
            {
                var product = new ProductCartDTO(row);
                productsCatalogId.Add(product.ProductCatalogId.ToString());

                var productKey = $"userCart:{userId}:{product.ProductCatalogId}";
                await _redisConnection.GetConnection()
                    .HashSetAsync(productKey, product.ToHashEntries());

                var addQuery = await _cassandraConnection.GetConnection()
                    .PrepareAsync(@"INSERT INTO cart (id, moment, userId, imageurl, price, productcatalogid, productname, quantity) 
                                        VALUES (?,?,?,?,?,?,?,?)");

                _cassandraConnection.GetConnection()
                    .Execute(addQuery.Bind(processId, DateTime.Now, userId, product.ImageURL, product.Price,
                        product.ProductCatalogId, product.ProductName, product.Quantity));
            }

            await _redisConnection.GetConnection()
                .StringSetAsync($"userCart:{userId}", string.Join(",", productsCatalogId));
        }
    }
}
