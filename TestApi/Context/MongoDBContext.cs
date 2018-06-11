using System;
using MongoDB.Driver;

namespace TestApi.Context
{
    public class MongoDBContext
    {
		public static string ConnectionString { get; set; }
		public static string DatabaseName { get; set; }
		private IMongoDatabase _database { get; }

        public MongoDBContext()
        {
			try {
				MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
				var mongoClient = new MongoClient(settings);
				_database = mongoClient.GetDatabase(DatabaseName);
			} catch (Exception ex) {
				throw new Exception("Can not access database", ex);
			}
        }
    }
}
