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
		public static void sendMessage(Stream stream, Object message)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, message);
		}

		/// <summary>
		/// Receive a message on a stream by deserializing its binary content
		/// </summary>
		/// <param name="stream">Stream to deserialize messages from</param>
		/// <returns>Deserialized message</returns>
		public static Object receiveMessage(Stream stream)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return (Object) binaryFormatter.Deserialize(stream);
		}
	}
}