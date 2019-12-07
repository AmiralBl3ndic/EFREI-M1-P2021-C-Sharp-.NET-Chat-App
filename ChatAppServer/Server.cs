using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatAppServer
{
	/// <summary>
	/// Backend server for the ChatApp project
	/// </summary>
	public class Server
	{
		private int _port;

		/// <summary>
		/// Create an instance of server listening on a specific port
		/// </summary>
		/// <param name="port">Port to listen on</param>
		public Server(int port)
		{
			_port = port;
		}

		/// <summary>
		/// Start the server on a specific port
		/// </summary>
		/// <param name="port">Port to start listening on</param>
		public void Start(int port)
		{
			_port = port;
			Start();
		}

		/// <summary>
		/// Start the server on already set port
		/// </summary>
		public void Start()
		{
			// Create a TCP listener on specified port and start listening
			TcpListener listener = new TcpListener(new IPAddress(new byte[] {127, 0, 0, 1}), _port);
			listener.Start();
			
			Console.WriteLine($"Server started on port {_port}");

			// Main thread loop, always accept new clients
			while (true)
			{
				// Wait for a new client connection
				TcpClient client = listener.AcceptTcpClient();
				
				Console.WriteLine("New client connected");
				
				// Start a new Thread to handle interactions with the client without blocking the main thread
				new Thread(new ClientHandler(client).Payload).Start();
			}
		}
	}
}