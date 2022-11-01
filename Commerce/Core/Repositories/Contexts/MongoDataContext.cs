namespace Commerce.Core.Repositories.Contexts
{
    using Commerce.Core.Repositories.Contexts.Interfaces;
    using MongoDB.Driver;

    public class MongoDataContext : IConnection<IMongoDatabase>
    {
        // SEE: https://www.youtube.com/watch?v=exXavNOqaVo&ab_channel=IAmTimCorey

        private MongoClient _client;

        private string DataBaseName;

        public MongoDataContext(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetValue<string>("Databases:Mongo:ConnectionString"));
            DataBaseName = configuration.GetValue<string>("Databases:Mongo:Database");
        }

        public IMongoDatabase GetConnection()
        {
            return _client.GetDatabase(DataBaseName);
        }
    }
}
