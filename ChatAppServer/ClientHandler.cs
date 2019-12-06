using System;
using System.Net.Sockets;
using System.Threading;

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
				Thread.Sleep(10000);  // TODO: implement logic here
			}
		}
	}
}