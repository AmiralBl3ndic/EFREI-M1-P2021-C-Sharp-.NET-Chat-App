# EFREI M1 P. 2021 - C# and .NET Environment | Multithreading TCP Chat Application

This project has been done for the EFREI M1 Software Engineering P.2021 C# and .NET Environment class.

It consits in 2 small applications communicating over TCP: a client (WPF application) allowing to chat with other chatters in a multi-topics environment, and a server (console application) allowing clients to communicate with eachother.

## Running the project

*This document was written for version **`1.0.2`** so these steps may vary a bit in future versions, but it is still good practice to follow them.*

1. Download the zipped binaries from the release section.
2. Unzip them anywhere you like, you must still be able to run programs from this location.
3. Run the `ChatApp/ChatApp Server/ChatAppServer.exe` application.
4. Once the server console is opened, run the `ChatApp/ChatApp Client/GUI Client.exe` application.

To open a new client, just run the `GUI Client.exe` application again without closing the previous window.

## Server 

### Short explanation

The project uses a console application as a server. 
It opens a TCP listener on port 4321 of the machine running it and awaits for connections.

One a client connects to the server, a dedicated thread will be created allowing to stay fluid when the number of clients increases.

Messages are sent from the clients to the server using serialization over a TCP stream of objects of `Command` class. This class represents a command and its arguments, just like in any CLI application.

### Available commands

The commands that are currently understood by the server are the following:

- `login {username} {password}`: Log into an existing account
- `register {username} {password}`: Create a new account and log into it
- `logout`: Logout from the account you are currently logged in
- `list-topics`: Get a list of all the topics available
- `create-topic {topic-name}`: Create and join a new topic (name must be unique)
- `join {topic-name}`: Join an existing topic
- `leave {topic-name}`: Leave a topic you have already joined
- `say {topic-name} {message}`: Say something to all users who joined the specified topic
- `dm {username} {message}`: Send a private message to specified user. If this user exists and isn't connected, message will be delivered upon this user's next connection

*More commands will be available in the future, but efforts are from now on focused on improving the client.*

### Responses

When a command needs one to many responses, the server will send them to the client using objects of the `Message` class.
This object contains the following properties:
- `Type`: Type of the message (enum: {Error, Info, Message})
- `Content`: Content of the message (string)

Theses properties allow the clients to understand the type of response they are receiving and to act accordingly.

### Persistence

User accounts data are stored along topics and private messages in a MongoDB database.

To ensure credentials safety, passwords are currently hashed using the BCrypt algorithm before being stored.

**WARNING**: private messages are only stored in the database when the receiver was not connected at the time the message was sent. As soon as the receiver connects, the private messages records are deleted from the database.

*In a future update, private messages will be encrypted before being stored to allow better privacy*

## Client

### Short explanation

The project comes provided with a GUI client. This client has been built using WPF and still needs improvements, but it allows sending commands to the server thanks to a text box where commands are awaited.

The upper part of the application consits in the actual chat zone where messages received from the server (thus, from other chatters) are displayed.
A separate thread allows the application to receive new messages without freezing so the experience should be freeze-free.

### Available commands

Since the client only serves the purpose to communicate with the server, the same commands are available for the client and the server.

However, because UX is important, I have added a `help` command which opens a dialog window where the list of all available commands and a short description of what they do is displayed. 

### Receiving messages

When receiving messages, the client takes advantage of the `Type` property of the `Message` class to apply specific styling to the received message. Hence, `Info` messages will be displayed differently than `Error` messages.
