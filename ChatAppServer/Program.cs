using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ChatAppServer
{
	class Program
	{
		static void Main(string[] args)
		{
			const string connectionString = "mongodb://localhost:27017";
			var mongoClient = new MongoClient(connectionString);

			IMongoDatabase db = mongoClient.GetDatabase("testDB");
			IMongoCollection<User> usersCollection = db.GetCollection<User>("usersCollection");

			var filter = new BsonDocument
			{
				{"username", "admin"}
			};

			var userFilter = new User {Username = "admin"};

			usersCollection.FindSync<User>(record => true).ForEachAsync(Console.WriteLine);
		}
	}


	class User
	{
		[BsonId] [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		
		[BsonElement("username")]
		public string Username { get; set; }
		
		[BsonElement("password")]
		public string Password { get; set; }

		public override string ToString()
		{
			return $"User {Id}(Username: {Username}, Password: {Password})";
		}
	}
}