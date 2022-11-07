namespace Cart.Core.Repositories.Contexts
{
    using Cassandra;
    using Cart.Core.Repositories.Contexts.Interfaces;

    public class CassandraDataContext : IConnection<ISession>
    {
        private ISession _session;

        public CassandraDataContext(IConfiguration configuration)
        {
            var keyspace = configuration.GetValue<string>("Databases:Cassandra:KeySpace");

            _session = Cluster.Builder()
                .AddContactPoint(configuration.GetValue<string>("Databases:Cassandra:ConnectionString"))
                .WithPort(configuration.GetValue<int>("Databases:Cassandra:Port"))
                .Build()
                .Connect();

            _session.CreateKeyspaceIfNotExists(keyspace);
            _session.ChangeKeyspace(keyspace);
        }

        public ISession GetConnection()
        {
            return _session;
        }
    }
}
