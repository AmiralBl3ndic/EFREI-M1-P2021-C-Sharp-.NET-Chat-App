using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using ChatAppServer.Models;
using ChatAppServer.Services;
using Communication;

namespace ChatAppServer
{
	/// <summary>
	/// Class handling logic for interacting with a client, meant to be instantiated and execute execute Payload as a Thread
	/// </summary>
	public partial class ClientHandler
	{
		private static Dictionary<User, TcpClient> ConnectedClients = new Dictionary<User, TcpClient>();
		
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
			// Loop forever (do not lose connection with client)
			while (true)
			{
				try
				{
					// Wait to receive command from client
					var command = Net.ReceiveCommand(_tcpClient.GetStream());
					if (command == null) continue;  // Perform only if command is not null

					// Initialize message
					var response = new Message();

					// Dispatch the command to the right handler
					switch (command.Name)
					{
						case "login":
							HandleLoginCommand(command, response);
							continue;
						
						case "register":
							HandleRegisterCommand(command, response);
							break;
						
						case "logout":
							HandleLogoutCommand(response);
							break;
						
						case "say":
							HandleSayCommand(command, response);
							continue;  // No need to go further
						
						case "list-topics":
							HandleListTopicsCommand(command, response);
							break;
						
						case "create-topic":
							HandleCreateTopicCommand(command, response);
							break;
						
						case "join":
							HandleJoinCommand(command, response);
							break;
						
						case "leave":
							HandleLeaveCommand(command, response);
							break;
						
						case "dm":
							HandleDmCommand(command, response);
							continue;  // No need to go further

						default:
							Console.WriteLine($"Unable to handle command: {command}");
							response.Type = MessageType.Error;
							response.Content = "Unknown operation";
							break;
					}
					
					Net.SendMessage(_tcpClient.GetStream(), response);
				}
				catch (IOException)  // Handle "losing" clients
				{
					// If user was authenticated, disconnect him
					if (_user != null)
					{
						ConnectedClients.Remove(_user);

						_user.LastSeenAt = DateTime.UtcNow;
						UserService.Update(_user.Id, _user);
					}
					
					Console.WriteLine("Client disconnected");
					return;	
				}
			}
		}
	}
}