using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

		private void SubmitCommand(string commandText)
		{
			var command = Command.Prepare(commandText);

			if (command == null)
			{
				DisplayError("Invalid command");
				return;
			}

			if (command.Error != null)
			{
				DisplayError(command.Error);
				return;
			}

			if (command.Name != "help")
			{
				Net.SendCommand(_tcpClient.GetStream(), command);
			}
			else
			{
				DisplayHelpDialog();
			}
		}
		
		private static void DisplayHelpDialog()
		{
			MessageBox.Show(
"Available commands:\n\n" +
	        "help:  Displays this window\n" +
	        "login {username} {password}:  Log into your account\n" +
	        "register {username} {password}:  Create an account\n" +
	        "list-topics:  Get the list of all topics\n" +
	        "create-topic:  Create a topic\n" +
	        "join {topic-name}:  Join a topic\n" +
	        "leave {topic-name}:  Leave a topic\n" +
	        "say {topic} {message}:  Say something in a topic\n" +
	        "dm {username} {message}:  Send a private message to the specified user\n" +
	        "logout:  Disconnect from your account",
				"Help",
				MessageBoxButton.OK,
				MessageBoxImage.Information);
		}


		private void DisplayError(string error)
		{
			var errorBlock = new TextBlock() {Text = error, Foreground = Brushes.DarkRed};
			MessagesPanel.Children.Add(errorBlock);
		}
	}
}