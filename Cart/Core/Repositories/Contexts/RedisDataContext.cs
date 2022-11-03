namespace Cart.Core.Contexts
{
    using Cart.Core.Repositories.Contexts.Interfaces;
    using StackExchange.Redis;

    public class RedisDataContext : IConnection<IConnectionMultiplexer>
    {

        public ConnectionMultiplexer _connection;

        public RedisDataContext(IConfiguration configuration)
        {
            _connection = ConnectionMultiplexer.Connect(configuration.GetValue<string>("Databases:Redis:ConnectionString"));
        }

        public IConnectionMultiplexer GetConnection()
        {
            return _connection;
        }
    }
}
