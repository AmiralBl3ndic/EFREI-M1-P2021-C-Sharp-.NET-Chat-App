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
			_user = AuthenticationService.AuthenticateUser(command.Arguments[0], command.Arguments[1]);
							
			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "Wrong credentials.";
			}
			else
			{
				// Add client to list of connected clients
				ConnectedClients.Add(_user, _tcpClient);

				response.Type = MessageType.Info;
				response.Content = $"Logged in as {_user.Username}.";
			}
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
			
			_user = null;
			response.Type = MessageType.Info;
			response.Content = "Logged out.";
		}
	}
}