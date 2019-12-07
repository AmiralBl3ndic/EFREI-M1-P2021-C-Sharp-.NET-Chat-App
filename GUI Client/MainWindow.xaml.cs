using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using Communication;

namespace GUI_Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private TcpClient _tcpClient = null;
		
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;

			UserInputField.Focus();
			
			_tcpClient = new TcpClient(FindResource("ServerIp").ToString(), int.Parse(FindResource("ServerPort").ToString()));
			
			var serverListenerThread = new Thread(ListenToServer);
			serverListenerThread.SetApartmentState(ApartmentState.STA);
			serverListenerThread.IsBackground = true;
			serverListenerThread.Start();
		}

		private static void SubmitCommand(string commandText)
		{
			var command = Command.Prepare(commandText);
			Console.WriteLine(command);
		}
	}
}