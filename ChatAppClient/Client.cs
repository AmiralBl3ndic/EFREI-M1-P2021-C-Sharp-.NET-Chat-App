using System;
using System.Net.Sockets;
using Communication;

namespace ChatAppClient
{
	public class Client : TcpClient
	{
		public Client(string hostname, int port) : base(hostname, port)
		{
			Console.WriteLine("Connection established");
		}

		
		public void Start()
		{
			while (true)
			{
				Console.SetCursorPosition(0, Console.BufferHeight - 1);
				Console.Write("$ ");

				var command = Command.Prepare(Console.ReadLine());
				Net.SendCommand(GetStream(), command);
			}
		}
	}
}