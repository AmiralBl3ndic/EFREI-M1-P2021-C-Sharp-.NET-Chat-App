using ChatAppServer.Models;
using ChatAppServer.Services;
using MongoDB.Driver;

namespace ChatAppServer
{
	class Program
	{
		static void Main(string[] args)
		{
			const string connectionString = "mongodb+srv://cs_dotnet_project:EFREI-2021-SE-4LIFE@efrei-m1-2021-nrato.gcp.mongodb.net/test?retryWrites=true&w=majority";
			var mongoClient = new MongoClient(connectionString);

			IMongoDatabase db = mongoClient.GetDatabase("testDB");

			UserService.UsersCollection = db.GetCollection<User>("Users");
			
			
		}
	}
}