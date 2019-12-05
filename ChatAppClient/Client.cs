using System;
using System.Net.Sockets;

namespace ChatAppClient
{
	public class Client : TcpClient
	{
		private string _username;

		private string _password;

		public string Username
		{
			set => _username = value;
		}

		public string Password
		{
			set => _password = value;
		}

		public Client(string username, string password, string hostname, int port) : base(hostname, port)
		{
			Console.WriteLine("Connection established");
		}

		
		public void Start()
		{
			while (true)
			{
				Console.SetCursorPosition(0, Console.BufferHeight - 1);
				Console.Write("> ");

				var command = Console.ReadLine();

				
			}
		}
	}
}