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
using System.Runtime.InteropServices;
using AkaSurf_Chat_Client;

namespace Akasurf_Chat_Client
{
    public partial class Form_Client : Form
    {
        private ContextMenu trayMenu;
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr window, bool bInvert);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        private delegate void UpdateUsers(string name);
        private delegate void Newform(string name, bool turn);
        // Will hold the user name
        public string publicpassword;
        public string UserName = "Unknown";
        public StreamWriter swSender;
        public StreamReader srReceiver;
        public Minesweeper_Form Minesweeper_Form;
        public Snake_Form Snake_form;
        private TcpClient tcpServer;
        // Needed to update the form with messages from another thread
        private delegate void UpdateLogCallback(string strMessage);
        // Needed to set the form to a "disconnected" state from another thread
        private delegate void CloseConnectionCallback(string strReason);
        public delegate void UpdateImageCallBack(int x, int y, int data, string turn);
        private Thread thrMessaging;
        private IPAddress ipAddr;
        public bool Connected;
        //   public Minesweeper_Form playing_form;
        private string whoisonline;
        public Save_ID Settings;
        public Form_Client()
        {

            // On application exit, don't forget to disconnect first
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();

            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", OnApplicationExit);
            notifyIcon1.Icon = new Icon(SystemIcons.Application, 40, 40);
            notifyIcon1.ContextMenu = trayMenu;

            Settings = new Save_ID();
            txtIp.Text = Settings.IP;
            txtUser.Text = Settings.Name;
            txtPort.Text = Settings.Port;
        }
        // The event handler for application exit
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected)
            {
                // Closes the connections, streams, etc.
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // If we are not currently connected but awaiting to connect
            if (!Connected)
            {
                // Initialize the connection
                if (txtUser.Text.Trim() != "")
                    InitializeConnection();
                else
                    appendtextfunction("invalid username");
            }
            else // We are connected, thus disconnect
            {
                if (this.InvokeRequired)
                    this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { "Disconnected at user's request" });
                else
                    CloseConnection("Disconnected at user's request");
            }
        }

        private void InitializeConnection()
        {
            try
            {
                if (Nudge_checkbox.Checked)
                    Nudge_btn.Enabled = true;

                publicpassword = txtpassword.Text;
                Register_btn.Enabled = false;
                // Parse the IP address from the TextBox into an IPAddress object
                ipAddr = IPAddress.Parse(txtIp.Text);
                // Start a new TCP connections to the chat server
                tcpServer = new TcpClient();
                tcpServer.Connect(ipAddr, int.Parse(txtPort.Text));
                // Helps us track whether we're connected or not
                Connected = true;
                // Prepare the form
                UserName = txtUser.Text;
                txtpassword.Enabled = false;
                // Disable and enable the appropriate fields
                txtIp.Enabled = false;
                txtUser.Enabled = false;
                txtPort.Enabled = false;
                txtMessage.Enabled = true;
                btnSend.Enabled = true;
                btnConnect.Text = "Disconnect";
                // Send the desired username to the server
                swSender = new StreamWriter(tcpServer.GetStream());
                invite_btn.Enabled = true;
                //send connect protocol
                swSender.WriteLine(txtUser.Text);
                swSender.Flush();
                swSender.WriteLine(txtpassword.Text);
                swSender.Flush();
                swSender.WriteLine(Environment.UserName);
                swSender.Flush();
                // Start the thread for receiving messages and further communication
                thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
                thrMessaging.IsBackground = true;
                thrMessaging.Start();

                Settings.IP = txtIp.Text;
                Settings.Name = txtUser.Text;
                Settings.Port = txtPort.Text;
                Settings.Save();

            }
            catch
            {
                txtLog.AppendText("No Server found \r\n");
                CloseConnection("The server is offline / invalid ip/port");
            }
        }

        private void Updaterusers(string names)
        {
            Users_List.Items.Add(names);
            Users_List.SelectedItem = "All";
        }

        private void ReceiveMessages()
        {
            try
            {
                // Receive the response from the server
                srReceiver = new StreamReader(tcpServer.GetStream());
                // If the first character of the response is 1, connection was successful
                string ConResponse = srReceiver.ReadLine();
                // If the first character is a 1, connection was successful
                if (ConResponse[0] == '1')
                {
                    // Update the form to tell it we are now connected
                    //part of the protocol to see who is online already
                    whoisonline = srReceiver.ReadLine();
                    string temp = "";
                    for (int i = 0; i < whoisonline.Length; i++)
                    {
                        if (i == whoisonline.Length)
                        {
                            if (whoisonline[i] != '#')
                                temp += whoisonline[i];
                            Users_List.Invoke(new UpdateUsers(this.Updaterusers), temp);
                        }

                        if (whoisonline[i] == '#' && i != whoisonline.Length)
                        {
                            Users_List.Invoke(new UpdateUsers(this.Updaterusers), temp);
                            temp = "";
                        }
                        else
                            temp += whoisonline[i];

                        if (whoisonline[i] != '#' && i == whoisonline.Length - 1)
                        {

                            Users_List.Invoke(new UpdateUsers(this.Updaterusers), temp);
                        }
                    }
                    this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
                }

                else // If the first character is not a 1 (probably a 0), the connection was unsuccessful
                {
                    string Reason = "";
                    // Extract the reason out of the response message. The reason starts at the 3rd character
                    Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                    // Update the form with the reason why we couldn't connect
                    this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                    // Exit the method
                    return;
                }
            }
            catch
            {
                if (this.InvokeRequired)
                    this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { "Disconnected from server" });
                else
                    CloseConnection("Disconnected from server");
            }

            // While we are successfully connected, read incoming lines from the server
            while (Connected)
            {
                // Show the messages in the log TextBox
                try
                {
                    process_data();
                }
                catch
                {
                    if (this.InvokeRequired)
                        this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { "Disconnected from server" });
                    else
                        CloseConnection("Disconnected from server");
                    // MessageBox.Show(e1.Message);
                }

            }

        }

        private void process_data()
        {
            string gameorchat = srReceiver.ReadLine();
            if (gameorchat == "chat")
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
            if (gameorchat == "MinewsweeperGame")  //it was -->  game
            {
                string command = srReceiver.ReadLine();
                if (command == "clickpos")
                {
                    this.Invoke(new UpdateImageCallBack(Minesweeper_Form.Update_Image), new object[] { int.Parse(srReceiver.ReadLine()), int.Parse(srReceiver.ReadLine()), int.Parse(srReceiver.ReadLine()), (srReceiver.ReadLine()) });
                }
                if (command == "sendinvite")
                {
                    string name = srReceiver.ReadLine();
                    if (UserName != name)
                    {
                        DialogResult dialogResult = MessageBox.Show(name + " would you like to start a new game?", "New Game", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            swSender.WriteLine("incoming");
                            swSender.Flush();
                            swSender.WriteLine("MinewsweeperGame");
                            swSender.Flush();
                            swSender.WriteLine("acceptinvite");
                            swSender.Flush();
                        }
                        else
                        {
                            swSender.WriteLine("incoming");
                            swSender.Flush();
                            swSender.WriteLine("MinewsweeperGame");
                            swSender.Flush();
                            swSender.WriteLine("declineinvite");
                            swSender.Flush();
                        }
                    }
                }
                else if (command == "startgame")
                {
                    string name = srReceiver.ReadLine();
                    string turn = srReceiver.ReadLine();
                    this.Invoke(new Newform(create_minesweepergameform), new object[] { name, turn == "True" });
                }
                else if (command == "declineinvite")
                {
                    this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Your friend declined minewsweeper game." });
                    MessageBox.Show("Your friend has declined");
                }
                else if (command == "endgame")
                {
                    MessageBox.Show("Game ended: " + srReceiver.ReadLine(), "game over");
                    Minesweeper_Form.playing = false;
                }
            }
            if (gameorchat == "SnakeGame")
            {
                string command = srReceiver.ReadLine();
                if (command == "updateloc")
                {
                    this.Invoke(new Snake_Form.UpdateLocationCallback(Snake_form.UpdateLocation_Local), new object[] { int.Parse(srReceiver.ReadLine()), int.Parse(srReceiver.ReadLine()) });
                    this.Invoke(new Snake_Form.UpdateLocationCallback(Snake_form.UpdateLocation_Enemy), new object[] { int.Parse(srReceiver.ReadLine()), int.Parse(srReceiver.ReadLine()) });
                }
                if (command == "sendinvite")
                {
                    string name = srReceiver.ReadLine();

                    if (UserName != name)
                    {
                        DialogResult dialogResult = MessageBox.Show(name + " would you like to start a new game?", "New Game", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            swSender.WriteLine("incoming");
                            swSender.Flush();
                            swSender.WriteLine("SnakeGame");
                            swSender.Flush();
                            swSender.WriteLine("acceptinvite");
                            swSender.Flush();
                        }
                        else
                        {
                            swSender.WriteLine("incoming");
                            swSender.Flush();
                            swSender.WriteLine("SnakeGame");
                            swSender.Flush();
                            swSender.WriteLine("declineinvite");
                            swSender.Flush();
                        }
                    }
                }
                else if (command == "startgame")
                {
                    string name = srReceiver.ReadLine();
                    //   string turn = srReceiver.ReadLine();
                    this.Invoke(new Newform(create_Snakegameform), new object[] { name, true }); // true does nothing for right now
                                                                                                 //    this.Invoke(new Newform(create_gameform), new object[] { name, turn == "True" });
                }
                else if (command == "declineinvite")
                {
                    this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Your friend declined SnakeGame game." });
                    // MessageBox.Show("Your friend has declined");
                }
                else if (command == "endgame")
                {
                    MessageBox.Show("Game ended: " + srReceiver.ReadLine(), "game over");
                    Minesweeper_Form.playing = false;
                }
            }
            if (gameorchat == "MinesweeperGamechat") /// it was gamechat
            {
                Minesweeper_Form.Invoke(new UpdateLogCallback(Minesweeper_Form.UpdateLog), new object[] { srReceiver.ReadLine() });
            }
        }

        private void create_minesweepergameform(string name_opponent, bool turn)
        {
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(300, "Game", "I'm here, until the game is over", ToolTipIcon.Info);
            Minesweeper_Form = new Minesweeper_Form(this, name_opponent, turn);
            Minesweeper_Form.Show();
        }

        private void create_Snakegameform(string name_opponent, bool turn)
        {
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(300, "Game", "I'm here, until the game is over", ToolTipIcon.Info);
            Snake_form = new Snake_Form(this);
            Snake_form.Show();
        }

        private void UpdateLog(string strMessage)
        {
            // Append text also scrolls the TextBox to the bottom each time
            //  txtLog.AppendText(strMessage + "\r\n");        
            if (strMessage[0] == '\\')
            {
                if (strMessage.Contains("\\Nudge"))
                {
                    strMessage = strMessage.Substring(6, strMessage.Length - 6);
                    appendtextfunction(strMessage);

                    if (Nudge_checkbox.Checked)
                        Nudge();
                    return;
                }
                if (strMessage == "\\Exit")
                {
                    this.Close();
                    Application.Exit();
                }
                if (strMessage == "\\Disconnect")
                {
                    if (this.InvokeRequired)
                        this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { "You have been removed by the server..." });
                    else
                        CloseConnection("You have been removed by the server...");
                    return;
                }
                appendtextfunction(strMessage.Substring(1, strMessage.Length - 1));
                return;
            }
            string finaltext;
            if (Timestamps_checkbox.Checked)
                finaltext = (DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + "  " + strMessage + "\r\n";
            else
                finaltext = strMessage + "\r\n";
            // if (strMessage.Substring(0, strMessage.IndexOf(":")).Contains("whisper"))
            if (strMessage.Contains("whisper"))
            {
                int length = txtLog.TextLength;  // at end of text
                txtLog.AppendText(finaltext);
                txtLog.SelectionStart = length;
                txtLog.SelectionLength = (finaltext).Length;
                txtLog.SelectionColor = Color.Purple;
                if (GetForegroundWindow() != this.Handle)
                    notifyIcon1.ShowBalloonTip(200, "Chat", strMessage, ToolTipIcon.Info);
            }
            //   if (strMessage.Substring(0, strMessage.IndexOf(":")).Contains("General"))
            else if (strMessage.Contains("[General]"))
            {
                int length = txtLog.TextLength;  // at end of text
                txtLog.AppendText(finaltext);
                txtLog.SelectionStart = length;
                txtLog.SelectionLength = (finaltext).Length;
                txtLog.SelectionColor = Color.Gray;
            }
            else if (strMessage.Contains("SERVER"))
            {
                int length = txtLog.TextLength;  // at end of text
                txtLog.AppendText(finaltext);
                txtLog.SelectionStart = length;
                txtLog.SelectionLength = (finaltext).Length;
                txtLog.SelectionColor = Color.Green;
            }
            else if (strMessage.Contains(" has joined") || strMessage.Contains(" has left"))
            {
                int length = txtLog.TextLength;  // at end of text
                txtLog.AppendText(strMessage + "\r\n");
                txtLog.SelectionStart = length;
                txtLog.SelectionLength = (strMessage + "\r\n").Length;
                txtLog.SelectionColor = Color.Black;
                if (this.Visible == false)
                    notifyIcon1.ShowBalloonTip(200, "Chat", strMessage, ToolTipIcon.Info);
            }
            if (strMessage.Contains(" has joined"))
            {
                if (strMessage.Substring(0, strMessage.IndexOf(" has joined")) != UserName)
                    Users_List.Items.Add(strMessage.Substring(0, strMessage.IndexOf(" has joined")));
            }
            if (strMessage.Contains(" has left"))
            {
                if (strMessage.Substring(0, strMessage.IndexOf(" has left")) != UserName)
                    Users_List.Items.Remove(strMessage.Substring(0, strMessage.IndexOf(" has left")));
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txtMessage_KeyPress(sender, new KeyPressEventArgs((char)13));
        }

        private void SendMessage()
        {
            if (txtMessage.Text[0] != '/')
            {
                if (txtMessage.Lines.Length >= 1)
                {
                    swSender.WriteLine("incoming");
                    swSender.WriteLine("chat");
                    swSender.WriteLine(txtMessage.Text);
                    swSender.WriteLine(Users_List.SelectedItem);
                    swSender.Flush();
                    txtMessage.Lines = null;
                }
            }
            else
            {
                switch (txtMessage.Text)
                {
                    case "/clear":
                        txtLog.Text = "";
                        break;
                    case "/help":
                        appendtextfunction("help: client end commands e.g: /clear /exit more coming soon!");
                        break;

                    case "/exit":
                        Application.Exit();
                        return;

                    default:
                        appendtextfunction("Unknown command, type /help.");
                        break;
                }
            }
            txtMessage.Text = "";
        }

        private void CloseConnection(string Reason)
        {
            if (Reason == "")
                Reason = "Disconned from server...";
            invite_btn.Enabled = false;
            // Show the reason why the connection is ending
            Nudge_btn.Enabled = false;
            appendtextfunction(Reason + "\r\n");
            Connected = false;
            Register_btn.Enabled = true;
            // Enable and disable the appropriate controls on the form
            txtIp.Enabled = true;
            txtUser.Enabled = true;
            txtPort.Enabled = true;
            txtMessage.Enabled = false;
            btnSend.Enabled = false;
            txtpassword.Enabled = true;
            btnConnect.Text = "Connect";
            Users_List.Items.Clear();
            // Close the objects
            if (swSender != null)
                swSender.Close();
            if (srReceiver != null)
                srReceiver.Close();
            if (tcpServer != null)
                tcpServer.Close();
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the key is Enter 
            if (e.KeyChar == (char)13 && txtMessage.Text != "" && !txtMessage.Text.Contains("\\"))
            {
                e.Handled = true;
                SendMessage();
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            txtMessage_KeyPress(sender, new KeyPressEventArgs((char)13));
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            if (Notify_Checkbox.Checked && GetForegroundWindow() != this.Handle)
            {
                FlashWindow(this.Handle, true);
            }
        }

        private void Settings_Label_Click(object sender, EventArgs e)
        {
            if (txtIp.Visible)
            {
                txtIp.Visible = false;
                txtPort.Visible = false;
                portlabel.Visible = false;
                iplabel.Visible = false;
            }
            else
            {
                txtIp.Visible = true;
                txtPort.Visible = true;
                portlabel.Visible = true;
                iplabel.Visible = true;
            }
        }

        private void Nudge()
        {
            int xCoord = this.Left;
            int yCoord = this.Top;
            int rnd = 0;
            Random randomclass = new Random();
            for (int i = 0; i < 600; i++)
            {
                rnd = randomclass.Next(xCoord + 1, xCoord + 15);
                this.Left = rnd;
                rnd = randomclass.Next(yCoord + 1, yCoord + 15);
                this.Top = rnd;
            }
            xCoord = this.Left;
            yCoord = this.Top;
        }

        private void Users_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Users_List.SelectedItem == null)
            {
                Users_List.SelectedItem = "All";
            }
            if (Users_List.SelectedItem.ToString() == "All")
            {
                txtMessage.ForeColor = Color.Gray;
                Towho_label.ForeColor = Color.Gray;
                Towho_label.Text = "[General]";
            }
            else
            {
                txtMessage.ForeColor = Color.Purple;
                Towho_label.ForeColor = Color.Purple;
                Towho_label.Text = "[Whispering  " + Users_List.SelectedItem.ToString() + "]:";
            }
        }

        private void txtLog_TextChanged_1(object sender, EventArgs e)
        {
            if (Notify_Checkbox.Checked && GetForegroundWindow() != this.Handle)
                FlashWindow(this.Handle, true);
            this.txtLog.ScrollToCaret();
        }

        private void Register_btn_Click(object sender, EventArgs e)
        {
            if (txtpassword.Text.Trim() != "" && txtUser.Text.Trim() != "")
            {
                btnConnect_Click(sender, e);
            }
            else
            {
                appendtextfunction("Invalid account information. \r\n");
            }
        }

        private void Nudge_btn_Click(object sender, EventArgs e)
        {
            if (Users_List.SelectedItem.ToString() != "All")
            {
                txtMessage.Text = "\\Nudge";
                SendMessage();

                appendtextfunction("You sent " + Users_List.SelectedItem.ToString() + " a nudge!");
            }
        }

        private void appendtextfunction(string txt)
        {
            string finaltext;
            if (Timestamps_checkbox.Checked)
                finaltext = (DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + "  " + txt + "\r\n";
            else
                finaltext = txt + "\r\n";
            if (txtLog.InvokeRequired)
                txtLog.Invoke(new UpdateLogCallback(UpdateLog), finaltext);
            else
                UpdateLog(finaltext);
        }

        private void Nudge_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Nudge_checkbox.Checked)
            {
                if (Connected)
                    Nudge_btn.Enabled = true;
            }
            else
                Nudge_btn.Enabled = false;
        }

        private void invite_btn_Click(object sender, EventArgs e)
        {
            txtLog.AppendText("An invite to " + Users_List.SelectedItem.ToString() + " to start a minesweeper game has been sent. \r\n");
            if (Users_List.SelectedItem.ToString() != "All")
            {
                swSender.WriteLine("incoming");
                swSender.Flush();
                swSender.WriteLine("MinewsweeperGame");
                swSender.Flush();
                swSender.WriteLine("sendinvite");
                swSender.Flush();
                swSender.WriteLine(Users_List.SelectedItem.ToString());
                swSender.Flush();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            // notifyIcon1.ShowBalloonTip(1000);
            WindowState = FormWindowState.Normal;
        }

        private void Form_Login_Client_Resize(object sender, EventArgs e)
        {
            //if (FormWindowState.Minimized == WindowState)
            //{
            //    this.Hide();
            //    notifyIcon1.Visible = true;
            //    notifyIcon1.ShowBalloonTip(300, "Game", "I'm here, until the game is over", ToolTipIcon.Info);

            //}
        }

        private void button_invite_snake_Click(object sender, EventArgs e)
        {
            txtLog.AppendText("An invite to " + Users_List.SelectedItem.ToString() + " to start a snake game has been sent. \r\n");
            if (Users_List.SelectedItem.ToString() != "All")
            {
                swSender.WriteLine("incoming");
                swSender.Flush();
                swSender.WriteLine("SnakeGame");
                swSender.Flush();
                swSender.WriteLine("sendinvite");
                swSender.Flush();
                swSender.WriteLine(Users_List.SelectedItem.ToString());
                swSender.Flush();
            }
        }
    }
}
