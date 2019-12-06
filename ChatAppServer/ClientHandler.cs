using System;
using System.Net.Sockets;
using System.Threading;
using ChatAppServer.Models;

namespace ChatAppServer
{
	/// <summary>
	/// Class handling logic for interacting with a client, meant to be instantiated and execute execute Payload as a Thread
	/// </summary>
	public class ClientHandler
	{
		/// <summary>
		/// TCP client the client uses to communicate
		/// </summary>
		private TcpClient _tcpClient;

		/// <summary>
		/// User object representing the User database record of the current client 
		/// </summary>
		private User _user = null;
		

		/// <summary>
		/// Create an instance of ClientHandler
		/// </summary>
		/// <param name="client">TCP client to use for communicating with client</param>
		public ClientHandler(TcpClient client)
		{
			_tcpClient = _tcpClient;
		}


		/// <summary>
		/// Payload of the thread
		/// </summary>
		public void Payload()
		{
			Console.WriteLine("ClientHandler thread started");

			// Loop forever (do not lose connection with client)
			while (true)
			{
				Console.WriteLine($"Inside ClientHandler thread {_tcpClient}");
				Thread.Sleep(3000);  // TODO: implement logic here
			}
		}
	}
}