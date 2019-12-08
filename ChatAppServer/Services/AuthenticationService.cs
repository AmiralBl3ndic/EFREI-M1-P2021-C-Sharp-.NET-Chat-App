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

		/// <summary>
		/// Create a user database record if the specified username doesn't already exist 
		/// </summary>
		/// <param name="username">Username of the user to create</param>
		/// <param name="password">Password of the user to create</param>
		/// <returns>User object representing the newly created record if success, null otherwise</returns>
		public static User CreateUser(string username, string password)
		{
			// Check if a user already exists with the specified username
			if (UserService.GetByUsername(username) != null)
			{
				return null;
			}
			
			var newUser = new User() {Username = username, Password = password};
			newUser.HashPassword();
			newUser.Topics.Add("general");

			return UserService.Create(newUser);
		}
	}
}