using ChatAppServer.Models;
using ChatAppServer.Services;
using MongoDB.Driver;

namespace ChatAppServer
{
	class Program
	{
		static void Main(string[] args)
		{
			// Initialize MongoDB database
			IMongoClient mongoDbClient = new MongoClient(Settings.MongoConnectionString);
			var db = mongoDbClient.GetDatabase(Settings.MongoDatabaseName);
			
			// Inject dependencies to services
			UserService.UsersCollection = db.GetCollection<User>(Settings.MongoUsersCollectionName);
			MessageRecordService.TopicMessagesCollection = db.GetCollection<TopicMessageRecord>(Settings.MongoMessagesCollectionName);
			MessageRecordService.PrivateMessagesCollection = db.GetCollection<PrivateMessageRecord>(Settings.MongoPrivateMessagesCollectionName);
			TopicsService.TopicsCollection = db.GetCollection<Topic>(Settings.MongoTopicsCollectionName);
			
			// Instantiate and start server
			var server = new Server(4321);
			server.Start();
		}
	}
}
