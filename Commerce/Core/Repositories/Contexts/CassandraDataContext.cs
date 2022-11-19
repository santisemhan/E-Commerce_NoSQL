namespace Commerce.Core.Repositories.Contexts
{
    using Cassandra;
    using Cassandra.Mapping;
    using Commerce.Core.Repositories.Contexts.Interfaces;

    public class CassandraDataContext : Mappings, IConnection<ISession>
    {
        // SEE: https://www.youtube.com/watch?v=GlDERX0B5HY&ab_channel=DevNinja

        private ISession _session;

        public CassandraDataContext(IConfiguration configuration)
        {
            _session = Cluster.Builder()
                         .WithCloudSecureConnectionBundle(@"C:\Users\gonza\Documents\Programacion\secure-connect-e-commerce-bd2.zip")
                         .WithCredentials(configuration.GetValue<string>("Databases:Cassandra:ClientID"), configuration.GetValue<string>("Databases:Cassandra:ClientSecret"))
                         .Build()
                         .Connect();

            _session.ChangeKeyspace(configuration.GetValue<string>("Databases:Cassandra:KeySpace"));
        }

        public ISession GetConnection()
        {
            return _session;
        }
    }
}
