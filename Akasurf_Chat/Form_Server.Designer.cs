namespace Akasurf_Chat
{
    partial class Form_Server
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
            this.Towho_label = new System.Windows.Forms.Label();
            this.Terminate_terimnal_btn = new System.Windows.Forms.Button();
            this.AccountMng_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userlimit_box = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Remove_User_Btn = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.Users_List = new System.Windows.Forms.ListBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.userlimit_box)).BeginInit();
            this.SuspendLayout();
            // 
            // Towho_label
            // 
            this.Towho_label.AutoSize = true;
            this.Towho_label.ForeColor = System.Drawing.Color.Gray;
            this.Towho_label.Location = new System.Drawing.Point(3, 308);
            this.Towho_label.Name = "Towho_label";
            this.Towho_label.Size = new System.Drawing.Size(53, 13);
            this.Towho_label.TabIndex = 40;
            this.Towho_label.Text = "[General]:";
            // 
            // Terminate_terimnal_btn
            // 
            this.Terminate_terimnal_btn.BackColor = System.Drawing.Color.Transparent;
            this.Terminate_terimnal_btn.Location = new System.Drawing.Point(375, 233);
            this.Terminate_terimnal_btn.Name = "Terminate_terimnal_btn";
            this.Terminate_terimnal_btn.Size = new System.Drawing.Size(75, 35);
            this.Terminate_terimnal_btn.TabIndex = 39;
            this.Terminate_terimnal_btn.Text = "Shut down Terminal";
            this.Terminate_terimnal_btn.UseVisualStyleBackColor = false;
            this.Terminate_terimnal_btn.Click += new System.EventHandler(this.Terminate_terimnal_btn_Click);
            // 
            // AccountMng_btn
            // 
            this.AccountMng_btn.Location = new System.Drawing.Point(375, 294);
            this.AccountMng_btn.Name = "AccountMng_btn";
            this.AccountMng_btn.Size = new System.Drawing.Size(75, 23);
            this.AccountMng_btn.TabIndex = 38;
            this.AccountMng_btn.Text = "Accounts";
            this.AccountMng_btn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(394, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Online:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(382, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "User limit:";
            // 
            // userlimit_box
            // 
            this.userlimit_box.Location = new System.Drawing.Point(386, 21);
            this.userlimit_box.Name = "userlimit_box";
            this.userlimit_box.Size = new System.Drawing.Size(39, 20);
            this.userlimit_box.TabIndex = 35;
            this.userlimit_box.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "IP:";
            // 
            // Remove_User_Btn
            // 
            this.Remove_User_Btn.Location = new System.Drawing.Point(375, 193);
            this.Remove_User_Btn.Name = "Remove_User_Btn";
            this.Remove_User_Btn.Size = new System.Drawing.Size(75, 34);
            this.Remove_User_Btn.TabIndex = 32;
            this.Remove_User_Btn.Text = "Disconnect Terminal";
            this.Remove_User_Btn.UseVisualStyleBackColor = true;
            this.Remove_User_Btn.Click += new System.EventHandler(this.Remove_User_Btn_Click);
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(375, 323);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 31;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Enabled = false;
            this.txtMessage.Location = new System.Drawing.Point(6, 326);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(360, 20);
            this.txtMessage.TabIndex = 30;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // Users_List
            // 
            this.Users_List.FormattingEnabled = true;
            this.Users_List.Location = new System.Drawing.Point(372, 68);
            this.Users_List.Name = "Users_List";
            this.Users_List.Size = new System.Drawing.Size(84, 121);
            this.Users_List.TabIndex = 29;
            this.Users_List.SelectedIndexChanged += new System.EventHandler(this.Users_List_SelectedIndexChanged);
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(275, 10);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(75, 23);
            this.btnListen.TabIndex = 28;
            this.btnListen.Text = "Go online";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(49, 6);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(100, 20);
            this.txtIp.TabIndex = 27;
            this.txtIp.Text = "127.0.0.1";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtLog.ForeColor = System.Drawing.SystemColors.Menu;
            this.txtLog.Location = new System.Drawing.Point(6, 55);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(360, 250);
            this.txtLog.TabIndex = 26;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(50, 29);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 25;
            this.txtPort.Text = "8001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(156, 29);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 43;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // Form_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 357);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Towho_label);
            this.Controls.Add(this.Terminate_terimnal_btn);
            this.Controls.Add(this.AccountMng_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userlimit_box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Remove_User_Btn);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.Users_List);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtPort);
            this.Name = "Form_Server";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.userlimit_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Towho_label;
        private System.Windows.Forms.Button Terminate_terimnal_btn;
        private System.Windows.Forms.Button AccountMng_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown userlimit_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Remove_User_Btn;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ListBox Users_List;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
    }
}

