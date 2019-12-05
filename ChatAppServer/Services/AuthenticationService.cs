using ChatAppServer.Models;

namespace ChatAppServer.Services
{
	public static class AuthenticationService
	{
		/// <summary>
		/// Authenticate a user from the database
		/// </summary>
		/// <param name="username">Username of the user to authenticate</param>
		/// <param name="password">Password of the user to authenticate</param>
		/// <returns>User record of the authenticated user if authentication succeeded, null otherwise</returns>
		public static User AuthenticateUser(string username, string password)
		{
			User userRecord = null;
			userRecord = UserService.GetByUsername(username);

			if (userRecord == null)
			{
				return null;
			}

			return BCrypt.Net.BCrypt.Verify(password, userRecord.Password) ? userRecord : null;
		}
	}
}