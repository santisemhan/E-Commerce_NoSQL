namespace Commerce.Core.Repositories.Contexts
{
    using Cassandra;
    using Cassandra.Mapping;
    using Commerce.Core.Repositories.Contexts.Interfaces;

    public class CassandraDataContext : Mappings, IConnection<ISession>
    {
        private ISession _session;

        public CassandraDataContext(IConfiguration configuration)
        {
            /*Borrar
            _session = Cluster.Builder()
                         .WithCloudSecureConnectionBundle(@"C:\Users\gonza\Documents\Programacion\secure-connect-e-commerce-bd2.zip")
                         .WithCredentials(configuration.GetValue<string>("Databases:Cassandra:ClientID"), configuration.GetValue<string>("Databases:Cassandra:ClientSecret"))
                         .Build()
                         .Connect();

            _session.ChangeKeyspace(configuration.GetValue<string>("Databases:Cassandra:KeySpace"));
            */
        }

        public ISession GetConnection()
        {
            return _session;
        }
    }
}
