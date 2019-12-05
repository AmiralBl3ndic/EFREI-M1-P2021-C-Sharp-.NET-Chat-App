using System;

namespace ChatAppClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the ChatApp client!\n");
			
			Console.WriteLine("Type \"help\" to see all the available commands");
			
			Client client = new Client("camboy", "abcdefgh", "127.0.0.1", 4321);
			client.Start();
		}
	}
}