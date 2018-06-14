using System;
using MongoDB.Driver;

namespace TestApi.Context
{
    public class MongoDBContext
    {
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }

        public IMongoDatabase getDatabase()
        {
			try {
				MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
				var mongoClient = new MongoClient(settings);
				return mongoClient.GetDatabase(DatabaseName);
			} catch (Exception ex) {
				throw new Exception("Can not access database", ex);
			}
        }
    }
}
