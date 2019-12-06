using System;
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
			
			// Instantiate and start server
			var server = new Server(4321);
			server.Start();
		}

		/// <summary>
		/// Simple testing code to be removed later on
		/// </summary>
		static void Demo()
		{
			var mongoClient = new MongoClient(Settings.MongoConnectionString);
      IMongoDatabase db = mongoClient.GetDatabase(Settings.MongoDatabaseName);
      
      UserService.UsersCollection = db.GetCollection<User>(Settings.MongoUsersCollectionName);
      
      User u1 = new User{Username = "admin", Password = "password"};
      User u2 = new User{Username = "testUser", Password = "test1234"};
      User u3 = new User{Username = "johndoe", Password = "mydogsname"};
      
      u1.HashPassword();
      u2.HashPassword();
      u3.HashPassword();
      
      UserService.Remove(u1);
      UserService.Remove(u2);
      UserService.Remove(u3);

      UserService.Create(u1);
      UserService.Create(u2);
      UserService.Create(u3);

      UserService.GetAll().ForEach(Console.WriteLine);

      while (true)
      {
      	Console.Write("Username: ");
      	string username = Console.ReadLine();
      	Console.Write("Password: ");
      	string password = Console.ReadLine();

      	User user = AuthenticationService.AuthenticateUser(username, password);

      	if (user == null)
      	{
      		Console.WriteLine("Wrong username/password");
      	}
      	else
      	{
      		Console.WriteLine("Authentication succeeded!");
      	}
      }
		}
	}
}
