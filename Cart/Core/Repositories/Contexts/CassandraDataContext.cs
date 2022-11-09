namespace Cart.Core.Repositories.Contexts
{
    using Cassandra;
    using Cart.Core.Repositories.Contexts.Interfaces;

    public class CassandraDataContext : IConnection<ISession>
    {
        private ISession _session;

        public CassandraDataContext(IConfiguration configuration)
        {
            _session = Cluster.Builder()
                        .WithCloudSecureConnectionBundle(@"C:\.Proyectos\secure-connect-e-commerce-bd2.zip")
                        .WithCredentials(configuration.GetValue<string>("Databases:Cassandra:ClientID"), configuration.GetValue<string>("Databases:Cassandra:ClientSecret"))
                        .Build()
                        .Connect();
        }

        public ISession GetConnection()
        {
            return _session;
        }
    }
}
