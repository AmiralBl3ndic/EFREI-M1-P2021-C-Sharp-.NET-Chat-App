using ChatAppServer.Services;
using Communication;

namespace ChatAppServer
{
	public partial class ClientHandler
	{
		/// <summary>
		/// Handle the Login command
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
				response.Content = "Wrong credentials";
			}
			else
			{
				response.Content = $"Logged in as {_user.Username}";
			}
		}
	}
}