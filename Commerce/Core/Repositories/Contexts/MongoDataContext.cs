namespace Commerce.Core.Repositories.Contexts
{
    using Commerce.Core.Repositories.Contexts.Interfaces;
    using MongoDB.Driver;

    public class MongoDataContext : IConnection<IMongoDatabase>
    {
        // SEE: https://www.youtube.com/watch?v=exXavNOqaVo&ab_channel=IAmTimCorey

        private MongoClient _client;

        private string DataBaseName;

        public IMongoDatabase Database;

        public MongoDataContext()
        {
            _client = new MongoClient("mongodb://127.0.0.1:8959");
            Database = _client.GetDatabase("CommerceDB");
        }

        /*
         * OBS: no se como usar el constructor de abajo
         * ¿Cuando instancio una clase con IConfiguration como paso el parametro?
         */

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
