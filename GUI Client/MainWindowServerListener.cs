using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Communication;

namespace GUI_Client
{
	public partial class MainWindow : Window
	{
		private void ListenToServer()
		{
			// Display a "connected to server" message
			Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
			{
				var connectedMessage = new TextBlock() {Text = $"Connected to server {(FindResource("ServerIp"))}:{(FindResource("ServerPort"))}"};
				connectedMessage.FontStyle = FontStyles.Italic;
				MessagesPanel.Children.Add(connectedMessage);
			}));
			
			// Listen to messages from server
			while (true)
			{
				var message = Net.ReceiveMessage(_tcpClient.GetStream());
				
				// Dispatcher needed because not in UI thread and trying to access UI components
				Dispatcher?.BeginInvoke(DispatcherPriority.Background, new Action(() =>
				{
					// Styling the message block before adding it to the list of messages
					var messageBlock = new TextBlock
					{
						Text = message.ToString(),
						Foreground = message.Type == MessageType.Error ? Brushes.Red : Brushes.Black,
						FontWeight = message.Type == MessageType.Error ? FontWeights.Bold : FontWeights.Regular
					};

					if (message.Type == MessageType.Info)
					{
						messageBlock.Foreground = Brushes.Blue;
						messageBlock.FontStyle = FontStyles.Italic;
					}
					
					MessagesPanel.Children.Add(messageBlock);
				}));
			}
		}
	}
}