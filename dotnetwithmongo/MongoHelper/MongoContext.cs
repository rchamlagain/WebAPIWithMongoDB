using MongoDB.Driver;

namespace dotnetwithmongo.MongoHelper
{
    public class MongoContext
    {
        public static string ConnectionString;
        public static string DatabaseName;
        public IMongoDatabase Database { get; }
       
        public MongoContext()
        {
            var client = new MongoClient(ConnectionString);
            if (client != null)
            {
                Database = client.GetDatabase(DatabaseName);
            }
        }
        public MongoContext(string connectionString,string DbName)
        {
            var client = new MongoClient(connectionString);
            if (client != null)
            {
                Database = client.GetDatabase(DbName);
            }
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }
    }
}
