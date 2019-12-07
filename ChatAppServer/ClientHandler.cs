using System;
using System.IO;
using System.Net.Sockets;
using ChatAppServer.Models;
using Communication;

namespace ChatAppServer
{
	/// <summary>
	/// Class handling logic for interacting with a client, meant to be instantiated and execute execute Payload as a Thread
	/// </summary>
	public partial class ClientHandler
	{
		/// <summary>
		/// TCP client the client uses to communicate
		/// </summary>
		private readonly TcpClient _tcpClient;

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
			_tcpClient = client;
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
				try
				{
					// Wait to receive command from client
					var command = Net.ReceiveCommand(_tcpClient.GetStream());
					if (command == null) continue;  // Perform only if command is not null
					
					Console.WriteLine($"Received command from client: {command}");
					
					// Initialize message
					var response = new Message();

					switch (command.Name)
					{
						case "login":
							HandleLoginCommand(command, response);
							break;
						
						case "register":
							HandleRegisterCommand(command, response);
							break;
						
						default:
							response.Type = MessageType.Error;
							response.Content = "Unknown operation";
							break;
					}
					
					Net.SendMessage(_tcpClient.GetStream(), response);
				}
				catch (IOException)  // Handle "losing" clients
				{
					Console.WriteLine("Client disconnected");
					return;	
				}
			}
		}
	}
}