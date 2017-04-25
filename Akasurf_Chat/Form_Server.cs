using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Timers;

namespace Akasurf_Chat
{
    public partial class Form_Server : Form
    {
        public static int USERLIMIT;
        public static string publicpassword;
        private bool Connected = false;
        private delegate void UpdateStatusCallback(string strMessage);
        public static Hashtable Games;

        public Form_Server()
        {
            InitializeComponent();
            ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
        }

        public static void CreateNewMinewsweeperGame(Connection player1, Connection player2)
        {
            Minesweeper_GameBoard board = new Minesweeper_GameBoard(player1, player2);
            Games.Add(board, board);
            player1.Minesweeper_Board = board;
            player2.Minesweeper_Board = board;
        }

        public static void CreateNewSnakeGame(Connection player1, Connection player2)
        {
            Snake_Game board = new Snake_Game(player1, player2);
            Games.Add(board, board);
            player1.Snake_Board = board;
            player2.Snake_Board = board;
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (Connected)
                goOffline(sender, e);
            else
                goOnline(sender, e);
        }

        private void goOnline(object sender, EventArgs e)
        {
            publicpassword = txtPassword.Text;
            userlimit_box.Enabled = false;
            USERLIMIT = (int)userlimit_box.Value;
            txtMessage.Enabled = true;
            Connected = true;
            btnSend.Enabled = true;
            txtIp.Enabled = false;
            txtPort.Enabled = false;
            txtPassword.Enabled = false;
            Games = new Hashtable();
            btnListen.Text = "Go offline";
            Users_List.Items.Add("All");
            Users_List.SelectedItem = "All";
            // Parse the server's IP address out of the TextBox
            IPAddress ipAddr = IPAddress.Parse(txtIp.Text);

            ChatServer.Init_Chatserver(ipAddr, int.Parse(txtPort.Text), Users_List);
            // Hook the StatusChangedf event handler to mainServer_StatusChanged
            // Start listening for connections
            ChatServer.StartListening();
            // Show that we started to listen for connections
            txtLog.AppendText("Server is online, awating connections...\r\n");
        }

        private void goOffline(object sender, EventArgs e)
        {
            userlimit_box.Enabled = true;
            txtMessage.Enabled = false;
            btnSend.Enabled = false;
            Connected = false;
            btnListen.Text = "Go Online";
            Users_List.Items.Clear();
            txtIp.Enabled = true;
            txtPort.Enabled = true;
            txtPassword.Enabled = true;
            txtLog.AppendText("Server is offline, server has been terminated...\r\n");

            while (Users_List.Items.Count > 2)
            {
                string user = Users_List.Items[1].ToString();
                ChatServer.SendAdminMessage(user + " Server is shutting down..");
                ChatServer.RemoveUser((Connection)ChatServer.ConncetedAccounts[user]);
            }
            ChatServer.Destory_Server();
        }

        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method tha/t updates the form
            try
            {
                this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
            }
        }

        private void UpdateStatus(string strMessage)
        {
            // Updates the log with the message
            txtLog.AppendText(strMessage + "\r\n");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txtMessage_KeyPress(sender, new KeyPressEventArgs((char)13));
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && txtMessage.Text != "")
            {
                e.Handled = true;
                if (Users_List.SelectedItem.ToString() != "All")
                {
                    ChatServer.ServerMessageTo("SERVER: " + txtMessage.Text, Users_List.SelectedItem.ToString());
                }
                else
                    ChatServer.SendAdminMessage("SERVER: " + txtMessage.Text);

                txtMessage.Text = "";
            }
        }

        private void Remove_User_Btn_Click(object sender, EventArgs e)
        {
            if (Users_List.SelectedItem == null)
                return;

            string user = Users_List.SelectedItem.ToString();
            ChatServer.SendAdminMessage(user + " has been removed by the server");
            ChatServer.RemoveUser((Connection)ChatServer.ConncetedAccounts[user]);
        }

        private void server_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Connected)
            {
                goOffline(sender, e);
            }
        }

        private void Terminate_terimnal_btn_Click(object sender, EventArgs e)
        {
            if (Users_List.SelectedItem == null)
                return;

            string user = Users_List.SelectedItem.ToString();
            ChatServer.SendAdminMessage(user + " has been removed by the server");
            ChatServer.ServerMessageTo("\\Exit", user);
        }

        public static void process_data(Connection player, string command, string data, string bomb)
        {
            ((Minesweeper_GameBoard)Games[player.Minesweeper_Board]).PlayerMove(player, command, data, bomb);
        }

        private void Users_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Users_List.SelectedItem == null)
                Users_List.SelectedItem = "All";
        }
    }

    public class StatusChangedEventArgs : EventArgs
    {
        // The argument we're interested in is a message describing the event
        private string EventMsg;
        // Property for retrieving and setting the event message
        public string EventMessage
        {
            get
            {
                return EventMsg;
            }
            set
            {
                EventMsg = value;
            }
        }
        // Constructor for setting the event message
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }

    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    public static class ChatServer
    {
        public static Hashtable ConncetedAccounts;
        public delegate void UserChange(Connection Account1, string action);
        private static IPAddress ipAddress;
        private static int PortNumber;
        private static TcpClient tcpClient;
        // The event and its argument will notify the form when a user has connected, disconnected, send message, etc.
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;
        public static ListBox User_List;
        // The thread that will hold the connection listener
        public static Thread thrListener;
        // The TCP object that listens for connections
        public static TcpListener tlsClient;
        // Will tell the while loop to keep monitoring for connections
        public static bool ServRunning;
        // The constructor sets the IP address to the one retrieved by the instantiating object
        public static void Init_Chatserver(IPAddress address, int portnum, ListBox User_List)
        {
            ConncetedAccounts = new Hashtable();
            PortNumber = portnum;
            ipAddress = address;
            ChatServer.User_List = User_List;
            ServRunning = false;
        }

        public static void UpdateUsers(Connection Account, string action)
        {

            if (Account == null || Account.currUser == null)
                return;


            if (action == "remove")
            {
                User_List.Items.Remove(Account.currUser);


                return;
            }



            if (action == "add")
            {
                User_List.Items.Add(Account.currUser);

                return;
            }

        }
        // Add the user to the hash tables
        public static void AddUser(Connection Account)
        {

            User_List.Invoke(new UserChange(ChatServer.UpdateUsers), new object[] { Account, "add" });


            // First add the username and associated connection to both hash tables
            ChatServer.ConncetedAccounts.Add(Account.currUser, Account);
            //    ChatServer.htUsers.Add(Account.currUser, Account.tcpClient);

            //   ChatServer.htConnections.Add(Account.tcpClient, Account.currUser);
            //  ChatServer.ConncetedAccounts.Add(Account);
            // Tell of the new connection to all other users and to the server form

            SendAdminMessage(Account.currUser + " has joined...");

        }
        // Remove the user from the hash tables
        public static void RemoveUser(Connection Account)
        {
            try
            {
                User_List.BeginInvoke(new UserChange(ChatServer.UpdateUsers), new object[] { Account, "remove" });
                if (ChatServer.ConncetedAccounts.ContainsKey(Account.currUser))
                {
                    SendAdminMessage(Account.currUser + " has left...");
                    ChatServer.ConncetedAccounts.Remove(Account.currUser);
                    Account.CloseConnection();
                }
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
            }
        }
        // This is called when we want to raise the StatusChanged event
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                // Invoke the delegate

                statusHandler(null, e);
            }
        }
        // Send administrative messages
        public static void SendAdminMessage(string Message)
        {
            StreamWriter swSenderSender;
            // First of all, show in our application who says what
            e = new StatusChangedEventArgs(Message);

            OnStatusChanged(e);
            foreach (Connection current_account in ChatServer.ConncetedAccounts.Values)
            {
                try
                {
                    // If the message is blank or the connection is null, break out
                    if (Message.Trim() == "" || current_account == null)
                    {
                        continue;
                    }
                    // Send the message to the current user in the loop
                    swSenderSender = new StreamWriter(current_account.tcpClient.GetStream());
                    swSenderSender.WriteLine("chat");
                    swSenderSender.WriteLine(Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch (Exception error)
                {
                    e = new StatusChangedEventArgs("\n" + error);
                    ChatServer.OnStatusChanged(e);
                    RemoveUser(current_account);
                }
            }
        }

        public static Connection getaccountbytcp(TcpClient tcpclient)
        {
            foreach (Connection item in ConncetedAccounts)
            {
                if (item.tcpClient == tcpclient)
                    return item;
            }
            return null;
        }

        public static Connection getaccountbyname(string name)
        {
            foreach (Connection item in ConncetedAccounts)
            {
                if (item.currUser == name)
                    return item;
            }
            return null;
        }
        // Send messages from one user to all the others

        public static void Broadcast(string From, string Message)
        {
            StreamWriter swSenderSender;
            // First of all, show in our application who says what
            e = new StatusChangedEventArgs(From + ": " + Message);

            OnStatusChanged(e);
            foreach (Connection current_account in ChatServer.ConncetedAccounts.Values)
            {

                try
                {
                    // If the message is blank or the connection is null, break out
                    if (Message.Trim() == "" || current_account == null)
                    {
                        continue;
                    }
                    // Send the message to the current user in the loop
                    swSenderSender = new StreamWriter(current_account.tcpClient.GetStream());
                    swSenderSender.WriteLine("chat");
                    swSenderSender.WriteLine("[General] " + From + ": " + Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch (Exception error)
                {
                    e = new StatusChangedEventArgs("\n" + error);
                    ChatServer.OnStatusChanged(e);
                    RemoveUser(current_account);
                }
            }
        }

        public static void SendSpecialCommand(string From, string Message, string To)
        {
            StreamWriter swSenderSender;
            // First of all, show in our application who says what
            e = new StatusChangedEventArgs(From + " To " + To + ": " + Message);
            OnStatusChanged(e);
            // Create an array of TCP clients, the size of the number of users we have
            Connection toaccount = (Connection)ChatServer.ConncetedAccounts[To];
            Connection fromaccount = (Connection)ChatServer.ConncetedAccounts[From];
            if (toaccount == null || fromaccount == null)
            {
                return;
            }
            try
            {
                // Send the message to the current user in the loop
                swSenderSender = new StreamWriter(toaccount.tcpClient.GetStream());
                if (Message == ("\\Nudge"))
                {
                    swSenderSender.WriteLine("chat");
                    swSenderSender.WriteLine("\\Nudge" + From + " sent you a nudge!");
                }
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                RemoveUser(toaccount);
            }

            try
            {
                swSenderSender = new StreamWriter(fromaccount.tcpClient.GetStream());
                if (Message == ("\\Nudge"))
                {
                    swSenderSender.WriteLine("chat");
                    swSenderSender.WriteLine("You sent you a nudge to " + To);
                }
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                RemoveUser(fromaccount);
            }
        }

        public static void SendMessageChat(string From, string Message, string To)
        {
            if (To == "All")
            {
                Broadcast(From, Message);
                return;
            }
            StreamWriter swSenderSender;
            // First of all, show in our application who says what
            e = new StatusChangedEventArgs(From + " To " + To + ": " + Message);
            OnStatusChanged(e);
            // Create an array of TCP clients, the size of the number of users we have
            Connection toaccount = (Connection)ChatServer.ConncetedAccounts[To];
            Connection fromaccount = (Connection)ChatServer.ConncetedAccounts[From];
            if (toaccount == null || fromaccount == null)
            {
                return;
            }
            try
            {
                // If the message is blank or the connection is null, break out
                // Send the message to the current user in the loop
                swSenderSender = new StreamWriter(toaccount.tcpClient.GetStream());
                swSenderSender.WriteLine("chat");
                swSenderSender.WriteLine(From + " whispers: " + Message);
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                RemoveUser(toaccount);
            }

            try
            {
                swSenderSender = new StreamWriter(fromaccount.tcpClient.GetStream());
                swSenderSender.WriteLine("chat");
                swSenderSender.WriteLine("whisper " + To + " : " + Message);
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                RemoveUser(fromaccount);
            }
        }

        public static void SendMessageTo_free(Connection To, string Message)
        {
            StreamWriter swSenderSender;
            //  e = new StatusChangedEventArgs(From + " To " + To + ": " + Message);
            //  OnStatusChanged(e);
            try
            {
                // If the message is blank or the connection is null, break out
                // Send the message to the current user in the loop
                swSenderSender = new StreamWriter(To.tcpClient.GetStream());
                swSenderSender.WriteLine(Message);
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch // If there was a problem, the user is not there anymore, remove him
            {
                RemoveUser(To);
            }
        }

        public static void SendMessageGameChat(string From, string Message, string To)
        {
            if (To == "All")
            {
                Broadcast(From, Message);
                return;
            }
            StreamWriter swSenderSender;
            // First of all, show in our application who says what
            e = new StatusChangedEventArgs(From + " To " + To + ": " + Message);
            OnStatusChanged(e);
            // Create an array of TCP clients, the size of the number of users we have
            Connection toaccount = (Connection)ChatServer.ConncetedAccounts[To];
            Connection fromaccount = (Connection)ChatServer.ConncetedAccounts[From];
            if (toaccount == null || fromaccount == null)
            {
                return;
            }
            try
            {
                // If the message is blank or the connection is null, break out
                // Send the message to the current user in the loop
                swSenderSender = new StreamWriter(toaccount.tcpClient.GetStream());
                swSenderSender.WriteLine("MinesweeperGamechat");
                swSenderSender.Flush();
                swSenderSender.WriteLine(From + ": " + Message);
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                RemoveUser(toaccount);
            }
        }

        public static void ServerMessageTo(string Message, string To)
        {
            StreamWriter swSenderSender;
            // First of all, show in our application who says what
            e = new StatusChangedEventArgs("SERVER To " + To + ": " + Message);
            OnStatusChanged(e);
            // Create an array of TCP clients, the size of the number of users we have
            Connection toaccount = (Connection)ChatServer.ConncetedAccounts[To];
            if (toaccount == null)
            {
                return;
            }
            try
            {
                // If the message is blank or the connection is null, break out
                // Send the message to the current user in the loop
                swSenderSender = new StreamWriter(toaccount.tcpClient.GetStream());
                swSenderSender.WriteLine("chat");
                swSenderSender.WriteLine(Message);
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                RemoveUser(toaccount);
            }
        }

        public static void Update_image_Client(Connection To, string x, string y, string data, string turn)
        {
            StreamWriter swSenderSender;
            // First of all, show in our application who says what
            // e = new StatusChangedEventArgs(From + " To " + To + ": " + Message);
            //  OnStatusChanged(e);
            // Create an array of TCP clients, the size of the number of users we have
            try
            {

                swSenderSender = new StreamWriter(To.tcpClient.GetStream());
                swSenderSender.WriteLine("MinewsweeperGame");
                swSenderSender.Flush();
                swSenderSender.WriteLine("clickpos");
                swSenderSender.Flush();
                swSenderSender.WriteLine(x.ToString());
                swSenderSender.Flush();
                swSenderSender.WriteLine(y.ToString());
                swSenderSender.Flush();
                swSenderSender.WriteLine(data.ToString());
                swSenderSender.Flush();
                swSenderSender.WriteLine(turn.ToString());
                swSenderSender.Flush();
                swSenderSender = null;
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                RemoveUser(To);
            }
        }

        public static void StartListening()
        {
            // Get the IP of the first network device, however this can prove unreliable on certain configurations
            // Create the TCP listener object using the IP of the server and the specified port
            //  tlsClient = new TcpListener(PortNumber);
            tlsClient = new TcpListener(ipAddress, PortNumber);
            // Start the TCP listener and listen for connections
            tlsClient.Start();
            // The while loop will check for true in this before checking for connections
            ServRunning = true;
            // Start the new tread that hosts the listener
            thrListener = new Thread(KeepListening);
            thrListener.IsBackground = true;
            thrListener.Start();
        }

        private static void KeepListening()
        {
            // While the server is running
            while (ServRunning)
            {
                // Accept a pending connection
                try
                {
                    if (!tlsClient.Pending())
                    {
                        Thread.Sleep(500);
                        continue;
                    }
                    tcpClient = tlsClient.AcceptTcpClient();
                    Connection newConnection = new Connection(tcpClient);
                }
                catch
                {
                    ServRunning = false;
                    tlsClient.Stop();
                }
            }
            tlsClient.Stop();
        }

        public static void Destory_Server()
        {
            int connected = ConncetedAccounts.Count;
            ChatServer.ServRunning = false;
        }

    }

    public class Connection
    {
        public Minesweeper_GameBoard Minesweeper_Board;
        public Connection Minewsweeper_partner;
        public Snake_Game Snake_Board;
        public Connection Snake_Game_partner;
        public TcpClient tcpClient;
        // The thread that will send information to the client
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string strResponse;
        // The thread that will send information to the client
        public string currUser;
        private string providedpassword;
        private static StatusChangedEventArgs e;
        private IPAddress accountip;
        private string windowsuser;
        // The constructor of the class takes in a TCP connection

        public Connection(TcpClient tcpCon)
        {
            this.tcpClient = tcpCon;
            // The thread that accepts the client and awaits messages
            thrSender = new Thread(AcceptClient);
            thrSender.IsBackground = true;
            // The thread calls the AcceptClient() method
            thrSender.Start();
        }

        public void CloseConnection()
        {
            // Close the currently open objects
            if (srReceiver != null)
                srReceiver.Close();
            if (swSender != null)
                swSender.Close();
            if (tcpClient != null)
                tcpClient.Close();
        }
        // Occures when a new client is accepted
        private string GetOnlineList()
        {
            string whoisonline = "All";
            if (ChatServer.ConncetedAccounts.Count == 0)
                whoisonline += "#";

            foreach (Connection item in ChatServer.ConncetedAccounts.Values)
            {
                whoisonline += "#" + item.currUser;
            }
            return whoisonline;
        }

        private void AcceptClient()
        {
            try
            {
                srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
                swSender = new System.IO.StreamWriter(tcpClient.GetStream());
                // Read the account information from the client
                currUser = srReceiver.ReadLine();
                this.providedpassword = srReceiver.ReadLine();
                windowsuser = srReceiver.ReadLine();
                accountip = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address;
                e = new StatusChangedEventArgs(accountip + " attemps to connect as \"" + currUser + "\" from: " + windowsuser);
                ChatServer.OnStatusChanged(e);
                // We got a response from the client
                if (currUser != "")
                {
                    // Store the user name in the hash table
                    if (currUser.Contains("SERVER"))
                    {
                        e = new StatusChangedEventArgs("Connection failed, invalid username. Username: \"" + currUser + "\"... by: " + windowsuser);
                        ChatServer.OnStatusChanged(e);
                        swSender.WriteLine("0|Account created sucessfuly!");
                        swSender.Flush();
                        CloseConnection();
                        return;
                    }
                    else if (providedpassword != Form_Server.publicpassword)
                    {
                        e = new StatusChangedEventArgs("Connection failed, incorrect password. Username: \"" + currUser + "\"... by: " + windowsuser);
                        ChatServer.OnStatusChanged(e);
                        swSender.WriteLine("0|Signing in failed. Please double-check your Username and re-enter your password.");
                        swSender.Flush();
                        CloseConnection();
                        return;
                    }
                    else if (ChatServer.ConncetedAccounts.ContainsKey(currUser))
                    {
                        // 0 means not connected
                        e = new StatusChangedEventArgs("Connection failed, username already logged. Username: \"" + currUser + "\"... by: " + windowsuser);
                        ChatServer.OnStatusChanged(e);
                        swSender.WriteLine("0|This username already exists.");
                        swSender.Flush();
                        CloseConnection();
                        return;
                    }
                    else
                    {
                        // 1 means connected successfully
                        swSender.WriteLine("1");
                        swSender.Flush();
                        swSender.WriteLine(GetOnlineList());
                        swSender.Flush();
                        // Add the user to the hash tables and start listening for messages from him
                        ChatServer.AddUser(this);
                    }
                }
                else
                {
                    e = new StatusChangedEventArgs("Connection Failed: connection protocol failed");
                    ChatServer.OnStatusChanged(e);
                    CloseConnection();
                    return;
                }
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                CloseConnection();
                return;
            }
            try
            {
                // Keep waiting for a message from the user
                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    // If it's invalid, remove the user
                    if (strResponse == null)
                    {
                        ChatServer.RemoveUser(this);
                    }
                    else
                    {
                        // Otherwise send the message to all the other users
                        Process_broadcast();
                    }
                }
            }
            catch (Exception error)
            {
                e = new StatusChangedEventArgs("\n" + error);
                ChatServer.OnStatusChanged(e);
                // If anything went wrong with this user, disconnect him
                ChatServer.ConncetedAccounts.Remove(this.currUser);
                this.CloseConnection();
            }
        }

        private void Process_broadcast()
        {
            string gameorchat = srReceiver.ReadLine();
            if (gameorchat == "MinewsweeperGame")
            {
                string gamecommand = srReceiver.ReadLine();
                if (gamecommand == "click_pos")
                {
                    ((Minesweeper_GameBoard)Form_Server.Games[this.Minesweeper_Board]).PlayerMove(this, srReceiver.ReadLine(), srReceiver.ReadLine(), srReceiver.ReadLine());
                }
                else if (gamecommand == "sendinvite")
                {
                    string towho = srReceiver.ReadLine();
                    this.Minewsweeper_partner = ((Connection)(ChatServer.ConncetedAccounts[towho]));
                    ((Connection)(ChatServer.ConncetedAccounts[towho])).Minewsweeper_partner = this;
                    ChatServer.SendMessageTo_free(((Connection)(ChatServer.ConncetedAccounts[towho])), "MinewsweeperGame");
                    ChatServer.SendMessageTo_free(((Connection)(ChatServer.ConncetedAccounts[towho])), "sendinvite");
                    ChatServer.SendMessageTo_free(((Connection)(ChatServer.ConncetedAccounts[towho])), currUser);
                    ChatServer.SendMessageTo_free(this, "MinewsweeperGame");
                    ChatServer.SendMessageTo_free(this, "getinvite");
                    ChatServer.SendMessageTo_free(this, currUser);
                }

                else if (gamecommand == "acceptinvite")
                {
                    Form_Server.CreateNewMinewsweeperGame(this.Minewsweeper_partner, this);
                    ChatServer.SendMessageTo_free(this, "MinewsweeperGame");
                    ChatServer.SendMessageTo_free(this, "startgame");
                    ChatServer.SendMessageTo_free(this, this.Minewsweeper_partner.currUser);
                    ChatServer.SendMessageTo_free(this, "False");
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, "MinewsweeperGame");
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, "startgame");
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, this.currUser);
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, "True");

                }
                else if (gamecommand == "declineinvite")
                {
                    //    Form_Server.CreateNewMinewsweeperGame(this.Minewsweeper_partner, this);
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, "MinewsweeperGame");
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, "declineinvite");
                }
                else if (gamecommand == "endgame")
                {
                    string reason = srReceiver.ReadLine();
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, "MinewsweeperGame");
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, "endgame");
                    ChatServer.SendMessageTo_free(this.Minewsweeper_partner, reason);
                }
            }
            if (gameorchat == "SnakeGame")
            {
                string gamecommand = srReceiver.ReadLine();

                if (gamecommand == "updateloc")
                {
                    ((Snake_Game)Form_Server.Games[this.Snake_Board]).Player_Move(this, (srReceiver.ReadLine()));


                }
                else if (gamecommand == "sendinvite")
                {
                    string towho = srReceiver.ReadLine();
                    ChatServer.SendMessageTo_free(((Connection)(ChatServer.ConncetedAccounts[towho])), "SnakeGame");
                    ChatServer.SendMessageTo_free(((Connection)(ChatServer.ConncetedAccounts[towho])), "sendinvite");
                    ChatServer.SendMessageTo_free(((Connection)(ChatServer.ConncetedAccounts[towho])), currUser);
                    this.Snake_Game_partner = ((Connection)(ChatServer.ConncetedAccounts[towho]));
                    ((Connection)(ChatServer.ConncetedAccounts[towho])).Snake_Game_partner = this;
                    ChatServer.SendMessageTo_free(this, "SnakeGame");
                    ChatServer.SendMessageTo_free(this, "getinvite");
                    ChatServer.SendMessageTo_free(this, currUser);
                }
                else if (gamecommand == "acceptinvite")
                {
                    ChatServer.SendMessageTo_free(this, "SnakeGame");
                    ChatServer.SendMessageTo_free(this, "startgame");
                    ChatServer.SendMessageTo_free(this, this.Snake_Game_partner.currUser);
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, "SnakeGame");
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, "startgame");
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, this.currUser);
                    Form_Server.CreateNewSnakeGame(this.Snake_Game_partner, this);
                }
                else if (gamecommand == "declineinvite")
                {
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, "SnakeGame");
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, "declineinvite");
                }
                else if (gamecommand == "endgame")
                {
                    string reason = srReceiver.ReadLine();
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, "SnakeGame");
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, "endgame");
                    ChatServer.SendMessageTo_free(this.Snake_Game_partner, reason);
                }
            }
            else if (gameorchat == "chat")
            {
                ChatServer.SendMessageChat(currUser, srReceiver.ReadLine(), srReceiver.ReadLine());
            }
            else if (gameorchat == "MinesweeperGamechat")
            {
                ChatServer.SendMessageGameChat(this.currUser, srReceiver.ReadLine(), this.Minewsweeper_partner.currUser.ToString());
            }
        }
    }

    public class Minesweeper_GameBoard
    {
        public static int ROWS = 16;
        public static int COLUMS = 16;
        public static int BOMBS = 41;
        public int player1_score;
        public int player2_score;
        public Connection Player1;
        public Connection Player2;
        public bool main_turn;
        public int[,] bord;
        public int[,] Discovered;
        public static int LIMIT_BOMBS = 3;
        public int[,] bombs;

        public Minesweeper_GameBoard(Connection player1, Connection player2)
        {
            LIMIT_BOMBS = BOMBS;
            bord = new int[ROWS, COLUMS];
            Discovered = new int[ROWS, COLUMS];
            this.bombs = new int[LIMIT_BOMBS, 2];
            PlaceBombs();
            PlaceNumbers();
            this.Player1 = player1;
            this.Player2 = player2;
            player1_score = 0;
            player2_score = 0;
            main_turn = true;
        }

        private void PlaceBombs()
        {
            Random r1 = new Random();
            for (int i = 0; i < LIMIT_BOMBS; i++)
            {
            again:
                bombs[i, 0] = r1.Next(bord.GetLength(0));
                bombs[i, 1] = r1.Next(bord.GetLength(1));

                if (bord[bombs[i, 0], bombs[i, 1]] == 20)
                    goto again;
                bord[bombs[i, 0], bombs[i, 1]] = 20;
            }
        }

        //private void PlaceNumbers()
        //{
        //    int offset_X;
        //    int offset_Y;
        //    for (int i = 0; i < LIMIT_BOMBS; i++)
        //    {
        //        offset_X = bombs[i, 0] - 1;
        //        offset_Y = bombs[i, 1];
        //        for (int j = -1; j < 2; j++)
        //        {
        //            try
        //            {
        //                bord[offset_X, offset_Y + j] += 1;
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        offset_X = bombs[i, 0];
        //        for (int j = -1; j < 2; j++)
        //        {
        //            if (j != 0)
        //            {
        //                try
        //                {
        //                    bord[offset_X, offset_Y + j] += 1;
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }
        //        offset_X = bombs[i, 0] + 1;
        //        for (int j = -1; j < 2; j++)
        //        {
        //            try
        //            {
        //                bord[offset_X, offset_Y + j] += 1;
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //}

        private void PlaceNumbers()
        {
            int offset_X;
            int offset_Y;

            for (int n = 0; n < LIMIT_BOMBS; n++)
            {

                for (int i = -1; i < 2; i++)
                {
                    offset_X = bombs[n, 0] + i;
                    if (offset_X < 0 || offset_X > ROWS - 1)
                        continue;

                    for (int j = -1; j < 2; j++)
                    {
                        offset_Y = bombs[n, 1] + j;

                        if (offset_Y < 0 || offset_Y > COLUMS - 1)
                            continue;

                        bord[offset_X, offset_Y]++;

                    }
                }
            }
        }

        public void PlayerMove(Connection player, string x, string y, string bomb)
        {
            if (player == Player1 && !main_turn)
                return;
            if (player == Player2 && main_turn)
                return;
            if (bomb == "True")
                UpdateBoard(int.Parse(x), int.Parse(y), true);
            else
                UpdateBoard(int.Parse(x), int.Parse(y), false);
        }

        private bool Check_Win()
        {
            if (((LIMIT_BOMBS - player1_score - player2_score + player2_score) <= player1_score))
            {
                ChatServer.SendMessageTo_free(Player1, "game");
                ChatServer.SendMessageTo_free(Player1, "endgame");
                ChatServer.SendMessageTo_free(Player1, Player1.currUser + " has won!");
                ChatServer.SendMessageTo_free(Player2, "game");
                ChatServer.SendMessageTo_free(Player2, "endgame");
                ChatServer.SendMessageTo_free(Player2, Player1.currUser + " has won!");
                return true;
            }
            if (((LIMIT_BOMBS - player1_score - player2_score) + player1_score) <= player2_score)
            {
                ChatServer.SendMessageTo_free(Player1, "game");
                ChatServer.SendMessageTo_free(Player1, "endgame");
                ChatServer.SendMessageTo_free(Player1, Player1.currUser + " has won!");
                ChatServer.SendMessageTo_free(Player2, "game");
                ChatServer.SendMessageTo_free(Player2, "endgame");
                ChatServer.SendMessageTo_free(Player2, Player1.currUser + " has won!");
                return true;
            }
            return false;
        }

        //private void Open_Zeros(int x, int y)
        //{
        //    int offset_x = x - 1;
        //    int offset_Y = y;
        //    for (int i = -1; i < 2; i++)
        //    {
        //        try
        //        {
        //            if (bord[offset_x, y + i] == 0 && Discovered[offset_x, y + i] == 0)
        //            {
        //                Discovered[offset_x, y + i] = 4;
        //                Open_Zeros(offset_x, y + i);
        //            }
        //            UpdateImage(offset_x, y + i);
        //        }
        //        catch { };
        //    }
        //    offset_x = x;
        //    offset_Y = y;
        //    try
        //    {

        //        if (bord[offset_x, y - 1] == 0 && Discovered[offset_x, y - 1] == 0)
        //        {
        //            Discovered[offset_x, y - 1] = 4;
        //            Open_Zeros(offset_x, y - 1);
        //        }
        //        UpdateImage(offset_x, y - 1);
        //    }
        //    catch { };
        //    try
        //    {
        //        if (bord[offset_x, y + 1] == 0 && Discovered[offset_x, y + 1] == 0)
        //        {
        //            Discovered[offset_x, y + 1] = 4;
        //            Open_Zeros(offset_x, y + 1);
        //        }
        //        UpdateImage(offset_x, y + 1);
        //    }
        //    catch { };
        //    offset_Y = y;
        //    offset_x = x + 1;
        //    for (int i = -1; i < 2; i++)
        //    {
        //        try
        //        {
        //            if (bord[offset_x, y + i] == 0 && Discovered[offset_x, y + i] == 0)
        //            {
        //                Discovered[offset_x, y + i] = 4;
        //                Open_Zeros(offset_x, y + i);
        //            }
        //            UpdateImage(offset_x, y + i);
        //        }
        //        catch { };
        //    }
        //}

        //private void open_zeross(int x, int y)
        //{
        //    for (int i = -1; i <= 1; i++)
        //    {
        //        for (int j = -1; j <= 1; j++)
        //        {
        //            try
        //            {
        //                if (bord[x + i, y + j] == 0 && Discovered[x + i, y + j] == 0)
        //                {
        //                    Discovered[x + i, y + j] = 1;
        //                    open_zeross(x + i, j + y);
        //                }
        //                UpdateImage(x + i, y + j);
        //            }
        //            catch
        //            { }
        //        }
        //    }
        //}

        private void Open_Zeros(int x, int y)
        {
            int offset_X;
            int offset_Y;


            for (int i = -1; i < 2; i++)
            {
                offset_X = x + i;
                if (offset_X < 0 || offset_X > ROWS - 1)
                    continue;

                for (int j = -1; j < 2; j++)
                {
                    offset_Y = y + j;

                    if (offset_Y < 0 || offset_Y > COLUMS - 1)
                        continue;


                    if (bord[offset_X, offset_Y] == 0 && Discovered[offset_X, offset_Y] == 0)
                    {
                        UpdateImage(offset_X, offset_Y);
                        Open_Zeros(offset_X, offset_Y);
                    }
                    else
                        UpdateImage(offset_X, offset_Y);

                }
            }

        }

        private void UpdateBoard(int x, int y, bool bomb)
        {
            if (Discovered[x, y] == 1)
                return;
            if (bomb)
            {
                int x_origin = x;
                int y_origin = y;
                for (int i = -2; i < 3; i++)
                {
                    for (int j = -2; j < 3; j++)
                    {
                        x = x_origin + i;
                        y = y_origin + j;
                        if (x < 0 || x > 15)
                            continue;
                        if (y < 0 || y > 15)
                            continue;
                        if (Discovered[x, y] == 1)
                            continue;
                        if (bord[x, y] == 0)
                        {
                            Open_Zeros(x, y);
                        }
                        //if its a bomb (bigger because the creator might add to 20);
                        if (bord[x, y] < 19 && !bomb)
                        {
                            main_turn = !main_turn;
                        }
                        if (bord[x, y] == 20)
                        {
                            if (main_turn)
                                player1_score++;
                            else
                                player2_score++;
                            Check_Win();
                        }
                        UpdateImage(x, y);
                    }
                }
                if (bomb)
                {
                    main_turn = !main_turn;
                }
            }
            else
            {
                if (bord[x, y] == 0)
                {
                    Open_Zeros(x, y);
                }
                //if its a bomb (bigger because the creator might add to 20);
                if (bord[x, y] < 19)
                {
                    main_turn = !main_turn;
                }
                else
                {
                    if (main_turn)
                        player1_score++;
                    else
                        player2_score++;
                    Check_Win();
                }
                UpdateImage(x, y);
            }
        }

        private void UpdateImage(int x, int y)
        {
            if (bord[x, y] < 19)
            {
                ChatServer.Update_image_Client(Player1, x.ToString(), y.ToString(), bord[x, y].ToString(), main_turn.ToString());
                ChatServer.Update_image_Client(Player2, x.ToString(), y.ToString(), bord[x, y].ToString(), (!main_turn).ToString());
            }
            else
            {
                if (main_turn)
                {
                    ChatServer.Update_image_Client(Player1, x.ToString(), y.ToString(), "11", main_turn.ToString());
                    ChatServer.Update_image_Client(Player2, x.ToString(), y.ToString(), "12", (!main_turn).ToString());
                }
                else
                {
                    ChatServer.Update_image_Client(Player1, x.ToString(), y.ToString(), "12", main_turn.ToString());
                    ChatServer.Update_image_Client(Player2, x.ToString(), y.ToString(), "11", (!main_turn).ToString());
                }
            }
            Discovered[x, y] = 1;
        }
    }

    public partial class Snake_Game
    {
        private Snake snake1;
        private Snake snake2;
        private Form baseform;
        private bool timer_Active = false;
        private Random rand_food = new Random();
        private int Lives = 3;
        private int level = 1;
        private System.Timers.Timer Game_Timer;
        private const int snake_dimension = 12;
        private Point bord_dimension = new Point(504, 348);
        private Point bord_Location = new Point(10, 10);
        Connection player1;
        Connection player2;
        public Snake_Game(Connection p1, Connection p2)
        {
            player1 = p1;
            player2 = p2;
            Reset_Game();
        }

        public void Game_Timer_tick(object sender, EventArgs e)
        {
            Snake_Move(sender, e, snake1);
            Snake_Move(sender, e, snake2);
            PickFood(sender, e);
            CheckforCollision(sender, e);



            ChatServer.SendMessageTo_free(player1, "incoming");
            ChatServer.SendMessageTo_free(player1, "SnakeGame");
            ChatServer.SendMessageTo_free(player1, "updateloc");
            ChatServer.SendMessageTo_free(player1, snake1.Head.X.ToString());
            ChatServer.SendMessageTo_free(player1, snake1.Head.Y.ToString());
            ChatServer.SendMessageTo_free(player1, snake2.Head.X.ToString());
            ChatServer.SendMessageTo_free(player1, snake2.Head.Y.ToString());


            ChatServer.SendMessageTo_free(player2, "incoming");
            ChatServer.SendMessageTo_free(player2, "SnakeGame");
            ChatServer.SendMessageTo_free(player2, "updateloc");
            ChatServer.SendMessageTo_free(player2, snake2.Head.X.ToString());
            ChatServer.SendMessageTo_free(player2, snake2.Head.Y.ToString());
            ChatServer.SendMessageTo_free(player2, snake1.Head.X.ToString());
            ChatServer.SendMessageTo_free(player2, snake1.Head.Y.ToString());
        }

        public void Snake_Move(object sender, EventArgs e, Snake snake)
        {
            snake.Head_old_location = snake.Head;
            switch (snake.direction_new)
            {
                case "Up":
                    if (snake.direction_old != "Down")
                    {

                        snake.Head.Y -= snake_dimension;
                        break;
                    }
                    snake.direction_old = snake.direction_new;
                    snake.Head.Y += snake_dimension;
                    break;

                case "Down":
                    if (snake.direction_old != "Up")
                    {
                        snake.Head.Y += snake_dimension;
                        break;
                    }
                    snake.direction_old = snake.direction_new;
                    snake.Head.Y -= snake_dimension;
                    break;
                case "Right":
                    if (snake.direction_old != "Left")
                    {
                        snake.Head.X += snake_dimension;
                        break;
                    }
                    snake.direction_old = snake.direction_new;
                    snake.Head.X -= snake_dimension;
                    break;
                case "Left":
                    if (snake.direction_old != "Right")
                    {
                        snake.Head.X -= snake_dimension;
                        break;
                    }
                    snake.direction_old = snake.direction_new;
                    snake.Head.X += snake_dimension;
                    break;
                default:
                    break;
            }
            if (snake.size > 0)
            {
                snake.snake_body.Enqueue(snake.Head_old_location);
                snake.snake_body.Dequeue();
            }
        }

        public void PickFood(object sender, EventArgs e)
        {
            if (Math.Abs(snake1.Head.X - snake1.food.X) < snake_dimension && Math.Abs(snake1.Head.Y - snake1.food.Y) < snake_dimension)
            {

                snake1.food.X = rand_food.Next(bord_Location.X + 10, bord_Location.X + bord_dimension.X - 20);
                snake1.food.Y = rand_food.Next(bord_Location.Y + 10, bord_Location.Y + bord_dimension.Y - 20);
                snake1.score += 2;
                snake1.size++;
                snake1.snake_body.Enqueue(snake1.snake_body.Peek());

            }

            if (Math.Abs(snake2.Head.X - snake2.food.X) < snake_dimension && Math.Abs(snake2.Head.Y - snake2.food.Y) < snake_dimension)
            {

                snake2.food.X = rand_food.Next(bord_Location.X + 10, bord_Location.X + bord_dimension.X - 20);
                snake2.food.Y = rand_food.Next(bord_Location.Y + 10, bord_Location.Y + bord_dimension.Y - 20);
                snake2.score += 2;
                snake2.snake_body.Enqueue(snake2.snake_body.Peek());
            }

            if (Math.Abs(snake1.Head.X - snake2.food.X) < snake_dimension && Math.Abs(snake1.Head.Y - snake2.food.Y) < snake_dimension)
            {

                snake1.food.X = rand_food.Next(bord_Location.X + 10, bord_Location.X + bord_dimension.X - 20);
                snake1.food.Y = rand_food.Next(bord_Location.Y + 10, bord_Location.Y + bord_dimension.Y - 20);
                snake1.score -= 1;

            }

            if (Math.Abs(snake2.Head.X - snake1.food.X) < snake_dimension && Math.Abs(snake2.Head.Y - snake1.food.Y) < snake_dimension)
            {

                snake2.food.X = rand_food.Next(bord_Location.X + 10, bord_Location.X + bord_dimension.X - 20);
                snake2.food.Y = rand_food.Next(bord_Location.Y + 10, bord_Location.Y + bord_dimension.Y - 20);
                snake2.score -= 1;
            }

            Win(sender, e);


        }

        public void Win(object sender, EventArgs e)
        {


            if (snake2.score == 10)
            {
                MessageBox.Show("Great! \n\n" + snake2.snake_name + " has won.", "Win", MessageBoxButtons.OK, MessageBoxIcon.Information);
                snake1.score = 0;
                snake2.score = 0;
                Stop_game(sender, e, "win");
            }

            if (snake1.score == 10)
            {
                MessageBox.Show("Great! \n\n" + snake1.snake_name + " has won.\n\n" + "Get ready for the next level", "Win", MessageBoxButtons.OK, MessageBoxIcon.Information);
                snake1.score = 0;
                snake2.score = 0;
                Stop_game(sender, e, "win");
            }



        }

        public void CheckforCollision(object sender, EventArgs e)
        {


            if (snake1.snake_body.Contains(snake1.Head))
                Stop_game(sender, e, "Snake 1 collied into himself");

            if (snake1.snake_body.Contains(snake2.Head))
                Stop_game(sender, e, "Snake 2 crushed into Snake 1");

            if (snake2.snake_body.Contains(snake1.Head))
                Stop_game(sender, e, "Snake 1 crushed into Snake 2");

            if (snake2.snake_body.Contains(snake2.Head))
                Stop_game(sender, e, "Snake 2 collied into himself");

            if (snake2.Head == snake1.Head)
                Stop_game(sender, e, "Snakes Collision!");


            //if (snake.snake_parts[0].Location.X > (Bord.Size.Width + 4))
            //{
            //    snake.snake_parts[0].Left = Bord.Left;

            //    if (snake.snake_parts[0].Top < Bord.Top + (Bord.Height / 2))
            //    {
            //        snake.snake_parts[0].Top += (Bord.Top + ((Bord.Height - 12) / 2) - snake.snake_parts[0].Top) * 2;
            //    }
            //    else
            //    {
            //        snake.snake_parts[0].Top -= Math.Abs((Bord.Top + ((Bord.Height - 12) / 2) - snake.snake_parts[0].Top) * 2);
            //    }


            //}
            //if (snake.snake_parts[0].Location.X < Bord.Location.X)
            //{
            //    snake.snake_parts[0].Left = Bord.Left + Bord.Width - 12;

            //    if (snake.snake_parts[0].Top <= Bord.Top + (Bord.Height / 2))
            //    {
            //        snake.snake_parts[0].Top += (Bord.Top + ((Bord.Height - 12) / 2) - snake.snake_parts[0].Top) * 2;
            //    }
            //    else
            //    {
            //        snake.snake_parts[0].Top -= Math.Abs((Bord.Top + ((Bord.Height - 12) / 2) - snake.snake_parts[0].Top) * 2);
            //    }

            //}

            if (snake1.Head.Y + 2 < bord_Location.Y || snake1.Head.Y + 12 > (bord_Location.Y + bord_dimension.Y) || snake1.Head.X + 2 < bord_Location.X || snake1.Head.X + 12 > (bord_Location.X + bord_dimension.X))
            {
                Stop_game(sender, e, "Snake1 collied with the wall ");
            }
            if (snake2.Head.Y + 2 < bord_Location.Y || snake2.Head.Y + 12 > (bord_Location.Y + bord_dimension.Y) || snake2.Head.X + 2 < bord_Location.X || snake2.Head.X + 12 > (bord_Location.X + bord_dimension.X))
            {
                Stop_game(sender, e, "Snake 2 collied with the wall ");
            }



        }

        public void Stop_game(object sender, EventArgs e, string reason)
        {
            Game_Timer.Enabled = false;
            timer_Active = false;
            Game_Timer.Stop();
        }

        public void Player_Move(Connection player, string e)
        {
            if (player1 == player)
            {

                if (e == "w")
                {
                    snake1.direction_old = snake1.direction_new;
                    snake1.direction_new = "Up";
                }
                if (e == "s")
                {
                    snake1.direction_old = snake1.direction_new;
                    snake1.direction_new = "Down";
                }
                if (e == "a")
                {
                    snake1.direction_old = snake1.direction_new;
                    snake1.direction_new = "Left";
                }
                if (e == "d")
                {
                    snake1.direction_old = snake1.direction_new;
                    snake1.direction_new = "Right";
                }
            }
            else
            {
                if (e == "w")
                {
                    snake2.direction_old = snake1.direction_new;
                    snake2.direction_new = "Up";
                }
                if (e == "s")
                {
                    snake2.direction_old = snake1.direction_new;
                    snake2.direction_new = "Down";
                }
                if (e == "a")
                {
                    snake2.direction_old = snake1.direction_new;
                    snake2.direction_new = "Left";
                }
                if (e == "d")
                {
                    snake2.direction_old = snake1.direction_new;
                    snake2.direction_new = "Right";
                }
            }
        }

        private void Reset_Game()
        {
            snake1 = new Snake("Snake1", new Point(203, 216), 3);
            snake2 = new Snake("Snake2", new Point(323, 216), 3);




            Game_Timer = new System.Timers.Timer();
            Game_Timer.Elapsed += new ElapsedEventHandler(Game_Timer_tick);
            // Game_Timer.Interval = 1000; //forcountdown
            //  this.Game_Timer.Tick += new System.EventHandler(this.Game_Timer_tick);
            Game_Timer.Interval = 400; //after countdown for actual game
            timer_Active = true;
            Game_Timer.Enabled = true;
            Game_Timer.Start();

        }
    }

    public class Snake
    {
        public string direction_old = "Up";
        public string direction_new = "Up";
        public string snake_name;
        public int score;
        public int Lives;
        public Point Head;
        public Point Head_old_location;
        public Point food;
        public Queue<Point> snake_body;
        public int size;
        public Snake(string name, Point initial_location, int initial_lives)
        {
            snake_body = new Queue<Point>();
            Head = initial_location;
            Head_old_location = initial_location;
            snake_name = name;
            Lives = initial_lives;
            size = 0;
        }
    }
}
