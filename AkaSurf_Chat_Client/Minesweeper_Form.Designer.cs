namespace Akasurf_Chat_Client
{
    partial class Minesweeper_Form
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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label_bombs_left = new System.Windows.Forms.Label();
            this.label_remote_score = new System.Windows.Forms.Label();
            this.label_local_score = new System.Windows.Forms.Label();
            this.label_vs = new System.Windows.Forms.Label();
            this.Label_Turn = new System.Windows.Forms.Label();
            this.buttom_bomb = new System.Windows.Forms.Button();
            this.button_cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Location = new System.Drawing.Point(6, 127);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(191, 243);
            this.txtLog.TabIndex = 43;
            this.txtLog.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(151, 376);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(46, 23);
            this.btnSend.TabIndex = 42;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 378);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(139, 20);
            this.txtMessage.TabIndex = 41;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // label_bombs_left
            // 
            this.label_bombs_left.AutoSize = true;
            this.label_bombs_left.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_bombs_left.Location = new System.Drawing.Point(56, 68);
            this.label_bombs_left.Name = "label_bombs_left";
            this.label_bombs_left.Size = new System.Drawing.Size(78, 17);
            this.label_bombs_left.TabIndex = 44;
            this.label_bombs_left.Text = "Bombs left:";
            // 
            // label_remote_score
            // 
            this.label_remote_score.AutoSize = true;
            this.label_remote_score.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_remote_score.ForeColor = System.Drawing.Color.Blue;
            this.label_remote_score.Location = new System.Drawing.Point(112, 40);
            this.label_remote_score.Name = "label_remote_score";
            this.label_remote_score.Size = new System.Drawing.Size(106, 17);
            this.label_remote_score.TabIndex = 45;
            this.label_remote_score.Text = "Enemy score: 0";
            // 
            // label_local_score
            // 
            this.label_local_score.AutoSize = true;
            this.label_local_score.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_local_score.ForeColor = System.Drawing.Color.Red;
            this.label_local_score.Location = new System.Drawing.Point(12, 40);
            this.label_local_score.Name = "label_local_score";
            this.label_local_score.Size = new System.Drawing.Size(93, 17);
            this.label_local_score.TabIndex = 46;
            this.label_local_score.Text = "Your score: 0";
            // 
            // label_vs
            // 
            this.label_vs.AutoSize = true;
            this.label_vs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_vs.Location = new System.Drawing.Point(66, 14);
            this.label_vs.Name = "label_vs";
            this.label_vs.Size = new System.Drawing.Size(67, 17);
            this.label_vs.TabIndex = 47;
            this.label_vs.Text = "You vs ...";
            // 
            // Label_Turn
            // 
            this.Label_Turn.AutoSize = true;
            this.Label_Turn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Label_Turn.Location = new System.Drawing.Point(54, 100);
            this.Label_Turn.Name = "Label_Turn";
            this.Label_Turn.Size = new System.Drawing.Size(91, 25);
            this.Label_Turn.TabIndex = 48;
            this.Label_Turn.Text = "Your turn";
            // 
            // buttom_bomb
            // 
            this.buttom_bomb.Location = new System.Drawing.Point(475, 11);
            this.buttom_bomb.Name = "buttom_bomb";
            this.buttom_bomb.Size = new System.Drawing.Size(75, 23);
            this.buttom_bomb.TabIndex = 49;
            this.buttom_bomb.Text = "Bomb";
            this.buttom_bomb.UseVisualStyleBackColor = true;
            this.buttom_bomb.Click += new System.EventHandler(this.buttom_bomb_Click);
            // 
            // button_cancel_button
            // 
            this.button_cancel_button.Enabled = false;
            this.button_cancel_button.Location = new System.Drawing.Point(394, 11);
            this.button_cancel_button.Name = "button_cancel_button";
            this.button_cancel_button.Size = new System.Drawing.Size(75, 23);
            this.button_cancel_button.TabIndex = 50;
            this.button_cancel_button.Text = "Cancel";
            this.button_cancel_button.UseVisualStyleBackColor = true;
            this.button_cancel_button.Click += new System.EventHandler(this.button_cancel_button_Click);
            // 
            // Minesweeper_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 410);
            this.Controls.Add(this.button_cancel_button);
            this.Controls.Add(this.buttom_bomb);
            this.Controls.Add(this.Label_Turn);
            this.Controls.Add(this.label_vs);
            this.Controls.Add(this.label_local_score);
            this.Controls.Add(this.label_remote_score);
            this.Controls.Add(this.label_bombs_left);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Name = "Minesweeper_Form";
            this.Text = "Form_Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Client_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label_bombs_left;
        private System.Windows.Forms.Label label_remote_score;
        private System.Windows.Forms.Label label_local_score;
        private System.Windows.Forms.Label label_vs;
        private System.Windows.Forms.Label Label_Turn;
        private System.Windows.Forms.Button buttom_bomb;
        private System.Windows.Forms.Button button_cancel_button;
    }
}