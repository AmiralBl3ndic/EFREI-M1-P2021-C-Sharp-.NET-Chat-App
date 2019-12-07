using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GUI_Client
{
	public partial class MainWindow : Window
	{
		private void ListenToServer()
		{
			Console.Write("Begin...");
			
			Console.WriteLine("end");

			Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
			{
				var connectedMessage = new TextBlock() {Text = $"Connected to server {(FindResource("ServerIp"))}:{(FindResource("ServerPort"))}"};
				connectedMessage.FontStyle = FontStyles.Italic;
				MessagesPanel.Children.Add(connectedMessage);
			}));
			
			while (true)
			{
				Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
				{
					var sampleMessage = new TextBlock() {Text = "I am a sample message"};
					MessagesPanel.Children.Add(sampleMessage);
				}));
				
				Thread.Sleep(1500);
			}
		}
	}
}