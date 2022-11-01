namespace Cart.Core.Contexts
{
    using Cart.Core.Repositories.Contexts.Interfaces;
    using Redis.OM;

    public class RedisDataContext : IConnection<RedisConnectionProvider>
    {
        // READ: https://redis.io/docs/stack/get-started/tutorials/stack-dotnet/

        public RedisConnectionProvider _provider;

        public RedisDataContext(IConfiguration configuration)
        {
            _provider = new RedisConnectionProvider(configuration.GetValue<string>("Databases:Redis:ConnectionString"));
        }

        public RedisConnectionProvider GetConnection()
        {
            return _provider;
        }
    }
}
