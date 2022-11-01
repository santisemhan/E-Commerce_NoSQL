namespace Commerce.Core.Repositories.Contexts
{
    using Cassandra;
    using Cassandra.Mapping;
    using Commerce.Core.Repositories.Contexts.Interfaces;

    public class CassandraDataContext : Mappings, IConnection<ISession>
    {
        // SEE: https://www.youtube.com/watch?v=GlDERX0B5HY&ab_channel=DevNinja

        private ICluster _cluster;

        private string KeySpaceName;

        public CassandraDataContext(IConfiguration configuration)
        {
            _cluster = Cluster.Builder()
                .AddContactPoint(configuration.GetValue<string>("Databases:Cassandra:ConnectionString"))
                .WithPort(configuration.GetValue<int>("Databases:Cassandra:Port"))
                .Build();

            KeySpaceName = configuration.GetValue<string>("Databases:Cassandra:KeySpace");

            // Example for entities
            // For<Post>().TableName("posts").PartitionKey(u => u.Id)
            //  .Column(x => x.Id)
            //  .Column(x => x.Title)
            //  .Column(x => x.Body)
        }

        public ISession GetConnection()
        {
            return _cluster.Connect(KeySpaceName);
        }
    }
}
