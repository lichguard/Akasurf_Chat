namespace Akasurf_Chat_Client
{
    partial class Form_Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Nudge_checkbox = new System.Windows.Forms.CheckBox();
            this.Towho_label = new System.Windows.Forms.Label();
            this.Register_btn = new System.Windows.Forms.Button();
            this.Nudge_btn = new System.Windows.Forms.Button();
            this.Timestamps_checkbox = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.Settings_Label = new System.Windows.Forms.Label();
            this.Notify_Checkbox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.portlabel = new System.Windows.Forms.Label();
            this.iplabel = new System.Windows.Forms.Label();
            this.Users_List = new System.Windows.Forms.ListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.invite_btn = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button_invite_snake = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Nudge_checkbox
            // 
            this.Nudge_checkbox.AutoSize = true;
            this.Nudge_checkbox.Checked = true;
            this.Nudge_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Nudge_checkbox.Location = new System.Drawing.Point(380, 256);
            this.Nudge_checkbox.Name = "Nudge_checkbox";
            this.Nudge_checkbox.Size = new System.Drawing.Size(58, 17);
            this.Nudge_checkbox.TabIndex = 45;
            this.Nudge_checkbox.Text = "Nudge";
            this.Nudge_checkbox.UseVisualStyleBackColor = true;
            // 
            // Towho_label
            // 
            this.Towho_label.AutoSize = true;
            this.Towho_label.ForeColor = System.Drawing.Color.Gray;
            this.Towho_label.Location = new System.Drawing.Point(13, 364);
            this.Towho_label.Name = "Towho_label";
            this.Towho_label.Size = new System.Drawing.Size(53, 13);
            this.Towho_label.TabIndex = 44;
            this.Towho_label.Text = "[General]:";
            // 
            // Register_btn
            // 
            this.Register_btn.Location = new System.Drawing.Point(179, 31);
            this.Register_btn.Name = "Register_btn";
            this.Register_btn.Size = new System.Drawing.Size(75, 23);
            this.Register_btn.TabIndex = 43;
            this.Register_btn.Text = "Register";
            this.Register_btn.UseVisualStyleBackColor = true;
            // 
            // Nudge_btn
            // 
            this.Nudge_btn.Enabled = false;
            this.Nudge_btn.Location = new System.Drawing.Point(382, 354);
            this.Nudge_btn.Name = "Nudge_btn";
            this.Nudge_btn.Size = new System.Drawing.Size(75, 23);
            this.Nudge_btn.TabIndex = 42;
            this.Nudge_btn.Text = "Send Nudge";
            this.Nudge_btn.UseVisualStyleBackColor = true;
            // 
            // Timestamps_checkbox
            // 
            this.Timestamps_checkbox.AutoSize = true;
            this.Timestamps_checkbox.Checked = true;
            this.Timestamps_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Timestamps_checkbox.Location = new System.Drawing.Point(380, 233);
            this.Timestamps_checkbox.Name = "Timestamps_checkbox";
            this.Timestamps_checkbox.Size = new System.Drawing.Size(84, 17);
            this.Timestamps_checkbox.TabIndex = 41;
            this.Timestamps_checkbox.Text = "TimeStamps";
            this.Timestamps_checkbox.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Location = new System.Drawing.Point(12, 59);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(362, 302);
            this.txtLog.TabIndex = 40;
            this.txtLog.Text = "";
            // 
            // Settings_Label
            // 
            this.Settings_Label.AutoSize = true;
            this.Settings_Label.Location = new System.Drawing.Point(264, 18);
            this.Settings_Label.Name = "Settings_Label";
            this.Settings_Label.Size = new System.Drawing.Size(45, 13);
            this.Settings_Label.TabIndex = 39;
            this.Settings_Label.Text = "Settings";
            // 
            // Notify_Checkbox
            // 
            this.Notify_Checkbox.AutoSize = true;
            this.Notify_Checkbox.Location = new System.Drawing.Point(380, 210);
            this.Notify_Checkbox.Name = "Notify_Checkbox";
            this.Notify_Checkbox.Size = new System.Drawing.Size(79, 17);
            this.Notify_Checkbox.TabIndex = 38;
            this.Notify_Checkbox.Text = "Notification";
            this.Notify_Checkbox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Password:";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(70, 33);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(100, 20);
            this.txtpassword.TabIndex = 36;
            this.txtpassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Online:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Name:";
            // 
            // portlabel
            // 
            this.portlabel.AutoSize = true;
            this.portlabel.Location = new System.Drawing.Point(320, 29);
            this.portlabel.Name = "portlabel";
            this.portlabel.Size = new System.Drawing.Size(29, 13);
            this.portlabel.TabIndex = 33;
            this.portlabel.Text = "Port:";
            // 
            // iplabel
            // 
            this.iplabel.AutoSize = true;
            this.iplabel.Location = new System.Drawing.Point(328, 6);
            this.iplabel.Name = "iplabel";
            this.iplabel.Size = new System.Drawing.Size(20, 13);
            this.iplabel.TabIndex = 32;
            this.iplabel.Text = "IP:";
            // 
            // Users_List
            // 
            this.Users_List.FormattingEnabled = true;
            this.Users_List.Location = new System.Drawing.Point(380, 70);
            this.Users_List.Name = "Users_List";
            this.Users_List.Size = new System.Drawing.Size(84, 134);
            this.Users_List.TabIndex = 31;
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(382, 381);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 30;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Enabled = false;
            this.txtMessage.Location = new System.Drawing.Point(12, 381);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(362, 20);
            this.txtMessage.TabIndex = 29;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(364, 26);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 28;
            this.txtPort.Text = "8001";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(179, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 27;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(70, 6);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 20);
            this.txtUser.TabIndex = 26;
            this.txtUser.Text = "user";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(364, 3);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(100, 20);
            this.txtIp.TabIndex = 25;
            this.txtIp.Text = "127.0.0.1";
            // 
            // invite_btn
            // 
            this.invite_btn.Enabled = false;
            this.invite_btn.Location = new System.Drawing.Point(379, 313);
            this.invite_btn.Name = "invite_btn";
            this.invite_btn.Size = new System.Drawing.Size(80, 35);
            this.invite_btn.TabIndex = 46;
            this.invite_btn.Text = "Invite Minesweeper";
            this.invite_btn.UseVisualStyleBackColor = true;
            this.invite_btn.Click += new System.EventHandler(this.invite_btn_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // button_invite_snake
            // 
            this.button_invite_snake.Location = new System.Drawing.Point(379, 275);
            this.button_invite_snake.Name = "button_invite_snake";
            this.button_invite_snake.Size = new System.Drawing.Size(80, 35);
            this.button_invite_snake.TabIndex = 47;
            this.button_invite_snake.Text = "Invite snake";
            this.button_invite_snake.UseVisualStyleBackColor = true;
            this.button_invite_snake.Click += new System.EventHandler(this.button_invite_snake_Click);
            // 
            // Form_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 410);
            this.Controls.Add(this.button_invite_snake);
            this.Controls.Add(this.invite_btn);
            this.Controls.Add(this.Nudge_checkbox);
            this.Controls.Add(this.Towho_label);
            this.Controls.Add(this.Register_btn);
            this.Controls.Add(this.Nudge_btn);
            this.Controls.Add(this.Timestamps_checkbox);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.Settings_Label);
            this.Controls.Add(this.Notify_Checkbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portlabel);
            this.Controls.Add(this.iplabel);
            this.Controls.Add(this.Users_List);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtIp);
            this.Name = "Form_Client";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form_Login_Client_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Nudge_checkbox;
        private System.Windows.Forms.Label Towho_label;
        private System.Windows.Forms.Button Register_btn;
        private System.Windows.Forms.Button Nudge_btn;
        private System.Windows.Forms.CheckBox Timestamps_checkbox;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label Settings_Label;
        private System.Windows.Forms.CheckBox Notify_Checkbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label portlabel;
        private System.Windows.Forms.Label iplabel;
        private System.Windows.Forms.ListBox Users_List;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Button invite_btn;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button_invite_snake;
    }
}

