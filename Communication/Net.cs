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
			var binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, message);
		}

		/// <summary>
		/// Receive a message on a stream by deserializing its binary content
		/// </summary>
		/// <param name="stream">Stream to deserialize messages from</param>
		/// <returns>Deserialized message</returns>
		public static Message ReceiveMessage(Stream stream)
		{
			var binaryFormatter = new BinaryFormatter();
			return (Message) binaryFormatter.Deserialize(stream);
		}

		/// <summary>
		/// Send a command on a stream 
		/// </summary>
		/// <param name="stream">Stream to send the command on</param>
		/// <param name="command">Command to send</param>
		public static void SendCommand(Stream stream, Command command)
		{
			var binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, command);
		}

		/// <summary>
		/// Receive a command on a stream
		/// </summary>
		/// <param name="stream">Stream to receive commands on</param>
		/// <returns>Received command</returns>
		public static Command ReceiveCommand(Stream stream)
		{
			var binaryFormatter = new BinaryFormatter();
			return (Command) binaryFormatter.Deserialize(stream);
		}
	}
}