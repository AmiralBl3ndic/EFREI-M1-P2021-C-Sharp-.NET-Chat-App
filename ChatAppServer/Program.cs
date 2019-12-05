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
			const string connectionString = "mongodb+srv://cs_dotnet_project:EFREI-2021-SE-4LIFE@efrei-m1-2021-nrato.gcp.mongodb.net/test?retryWrites=true&w=majority";
			var mongoClient = new MongoClient(connectionString);
			IMongoDatabase db = mongoClient.GetDatabase("testDB");

			
			UserService.UsersCollection = db.GetCollection<User>("Users");
			
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
