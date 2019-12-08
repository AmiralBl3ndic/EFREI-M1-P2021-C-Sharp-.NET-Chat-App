using System;
using System.Collections.Generic;
using System.Text;
using ChatAppServer.Models;
using ChatAppServer.Services;
using Communication;

namespace ChatAppServer
{
	public partial class ClientHandler
	{
		/// <summary>
		/// Handle the "login" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleLoginCommand(Command command, Message response)
		{
			// Try authentication
			var authUser = AuthenticationService.AuthenticateUser(command.Arguments[0], command.Arguments[1]);
			
			// Check if authentication succeeded
			if (authUser == null)
			{
				response.Type = MessageType.Error;
				response.Content = "Wrong credentials.";
				Net.SendMessage(_tcpClient.GetStream(), response);
				return;
			}

			// Check if a client is already connected to this account
			foreach (var user in ConnectedClients.Keys)
			{
				if (user.Username == authUser.Username)
				{
					response.Type = MessageType.Error;
					response.Content = "Another client is already connected to this account";
					Net.SendMessage(_tcpClient.GetStream(), response);
					return;
				}
			}
			
			_user = authUser;

			// Add client to list of connected clients
			ConnectedClients.Add(_user, _tcpClient);

			response.Type = MessageType.Info;
			response.Content = $"Logged in as {_user.Username}.";
			Net.SendMessage(_tcpClient.GetStream(), response);
			
			// Gather unread private messages
			List<PrivateMessageRecord> privateMessages = MessageRecordService.GetPrivateMessages(_user);

			//if (privateMessages.Count == 0) return;
			
			// Build the private messages block
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"=== You received {privateMessages.Count} private messages during your absence ===");
			foreach (var message in privateMessages)
			{
				sb.AppendLine($"[From: {message.Sender}] - {message.Content}");
			}
			sb.Append("=========================================");

			response.Type = MessageType.Message;
			response.Content = sb.ToString();
			Net.SendMessage(_tcpClient.GetStream(), response);
			
			// Clear the list of unread private messages
			MessageRecordService.ClearPrivateMessages(_user);
		}

		/// <summary>
		/// Handle the "register" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleRegisterCommand(Command command, Message response)
		{
			// Check if user is already authenticated
			if (_user != null)
			{
				response.Type = MessageType.Error;
				response.Content = "Cannot create accounts while logged in.";
				return;
			}
			
			// Attempt to create user
			_user = AuthenticationService.CreateUser(command.Arguments[0], command.Arguments[1]);

			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "Credentials already in use.";
			}
			else
			{
				// Add client to list of connected clients
				ConnectedClients.Add(_user, _tcpClient);

				response.Type = MessageType.Info;
				response.Content = $"Account created, you are now logged in as {_user.Username}.";
			}
		}

		/// <summary>
		/// Handle the "logout" command
		/// </summary>
		/// <param name="response">Message object to send to the user</param>
		private void HandleLogoutCommand(Message response)
		{
			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "Cannot log out: you are not logged in.";
				return;
			}

			// Remove user from list of connected clients
			ConnectedClients.Remove(_user);

			_user.LastSeenAt = DateTime.UtcNow;
			UserService.Update(_user.Id, _user);
			
			_user = null;
			response.Type = MessageType.Info;
			response.Content = "Logged out.";
		}
	}
}