using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Communication
{
	public class Net
	{
		/// <summary>
		/// Send a message on a stream by serializing it to binary before doing so
		/// </summary>
		/// <param name="stream">Stream to serialize the message on</param>
		/// <param name="message">Message to send</param>
		public static void SendMessage(Stream stream, Message message)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, message);
		}

		/// <summary>
		/// Receive a message on a stream by deserializing its binary content
		/// </summary>
		/// <param name="stream">Stream to deserialize messages from</param>
		/// <returns>Deserialized message</returns>
		public static Message ReceiveMessage(Stream stream)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return (Message) binaryFormatter.Deserialize(stream);
		}

		/// <summary>
		/// Send a message with authentication data (user password) by serializing it to binary before doing so
		/// </summary>
		/// <param name="stream">Stream to serialize the message on</param>
		/// <param name="message">Message to send</param>
		public static void SendAuthenticatedMessage(Stream stream, AuthenticatedMessage message)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, message);
		}

		/// <summary>
		/// Receive an authenticated message on a stream by deserializing its binary content
		/// </summary>
		/// <param name="stream">Stream to deserialize messages from</param>
		/// <returns>Deserialized AuthenticatedMessage</returns>
		public static AuthenticatedMessage ReceiveAuthenticatedMessage(Stream stream)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return (AuthenticatedMessage) binaryFormatter.Deserialize(stream);
		}
	}
}