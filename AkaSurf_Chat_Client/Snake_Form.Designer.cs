namespace Akasurf_Chat_Client
{
    partial class Snake_Form
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
            this.Score_Label = new System.Windows.Forms.Label();
            this.Level_Label = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.Start_label = new System.Windows.Forms.Label();
            this.Snake_Head = new System.Windows.Forms.PictureBox();
            this.Bord = new System.Windows.Forms.PictureBox();
            this.Food_Progress = new System.Windows.Forms.ProgressBar();
            this.Snake_Head2 = new System.Windows.Forms.PictureBox();
            this.Player1_Label = new System.Windows.Forms.Label();
            this.Player2_Label = new System.Windows.Forms.Label();
            this.P1_lives_label = new System.Windows.Forms.Label();
            this.P2_lives_label = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Score_Label2 = new System.Windows.Forms.Label();
            this.Food = new System.Windows.Forms.PictureBox();
            this.Food2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Snake_Head)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Snake_Head2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Food)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Food2)).BeginInit();
            this.SuspendLayout();
            // 
            // Score_Label
            // 
            this.Score_Label.AutoSize = true;
            this.Score_Label.Location = new System.Drawing.Point(63, 433);
            this.Score_Label.Name = "Score_Label";
            this.Score_Label.Size = new System.Drawing.Size(47, 13);
            this.Score_Label.TabIndex = 0;
            this.Score_Label.Text = "Score: 0";
            // 
            // Level_Label
            // 
            this.Level_Label.AutoSize = true;
            this.Level_Label.Location = new System.Drawing.Point(12, 32);
            this.Level_Label.Name = "Level_Label";
            this.Level_Label.Size = new System.Drawing.Size(45, 13);
            this.Level_Label.TabIndex = 1;
            this.Level_Label.Text = "Level: 1";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(145, 5);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(91, 31);
            this.Title.TabIndex = 2;
            this.Title.Text = "Snake";
            // 
            // Start_label
            // 
            this.Start_label.AutoSize = true;
            this.Start_label.BackColor = System.Drawing.Color.White;
            this.Start_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_label.Location = new System.Drawing.Point(238, 105);
            this.Start_label.Name = "Start_label";
            this.Start_label.Size = new System.Drawing.Size(70, 76);
            this.Start_label.TabIndex = 4;
            this.Start_label.Text = "5";
            // 
            // Snake_Head
            // 
            this.Snake_Head.BackColor = System.Drawing.Color.Black;
            this.Snake_Head.Location = new System.Drawing.Point(109, 216);
            this.Snake_Head.Name = "Snake_Head";
            this.Snake_Head.Size = new System.Drawing.Size(12, 12);
            this.Snake_Head.TabIndex = 9;
            this.Snake_Head.TabStop = false;
            // 
            // Bord
            // 
            this.Bord.BackColor = System.Drawing.Color.White;
            this.Bord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Bord.ErrorImage = null;
            this.Bord.InitialImage = null;
            this.Bord.Location = new System.Drawing.Point(15, 48);
            this.Bord.Name = "Bord";
            this.Bord.Size = new System.Drawing.Size(504, 348);
            this.Bord.TabIndex = 30;
            this.Bord.TabStop = false;
            this.Bord.WaitOnLoad = true;
            // 
            // Food_Progress
            // 
            this.Food_Progress.Location = new System.Drawing.Point(15, 402);
            this.Food_Progress.Name = "Food_Progress";
            this.Food_Progress.Size = new System.Drawing.Size(506, 12);
            this.Food_Progress.TabIndex = 32;
            // 
            // Snake_Head2
            // 
            this.Snake_Head2.BackColor = System.Drawing.Color.SkyBlue;
            this.Snake_Head2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Snake_Head2.Enabled = false;
            this.Snake_Head2.InitialImage = null;
            this.Snake_Head2.Location = new System.Drawing.Point(343, 200);
            this.Snake_Head2.Name = "Snake_Head2";
            this.Snake_Head2.Size = new System.Drawing.Size(12, 12);
            this.Snake_Head2.TabIndex = 34;
            this.Snake_Head2.TabStop = false;
            this.Snake_Head2.WaitOnLoad = true;
            // 
            // Player1_Label
            // 
            this.Player1_Label.AutoSize = true;
            this.Player1_Label.BackColor = System.Drawing.Color.White;
            this.Player1_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1_Label.Location = new System.Drawing.Point(170, 243);
            this.Player1_Label.Name = "Player1_Label";
            this.Player1_Label.Size = new System.Drawing.Size(91, 13);
            this.Player1_Label.TabIndex = 35;
            this.Player1_Label.Text = "Player 1: (a,s,d,w)";
            // 
            // Player2_Label
            // 
            this.Player2_Label.AutoSize = true;
            this.Player2_Label.BackColor = System.Drawing.Color.White;
            this.Player2_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2_Label.Location = new System.Drawing.Point(293, 243);
            this.Player2_Label.Name = "Player2_Label";
            this.Player2_Label.Size = new System.Drawing.Size(84, 13);
            this.Player2_Label.TabIndex = 36;
            this.Player2_Label.Text = "Player 2: (d,f,g,r)";
            // 
            // P1_lives_label
            // 
            this.P1_lives_label.AutoSize = true;
            this.P1_lives_label.Location = new System.Drawing.Point(45, 418);
            this.P1_lives_label.Name = "P1_lives_label";
            this.P1_lives_label.Size = new System.Drawing.Size(88, 13);
            this.P1_lives_label.TabIndex = 57;
            this.P1_lives_label.Text = "Player 1 Lives : 3";
            // 
            // P2_lives_label
            // 
            this.P2_lives_label.AutoSize = true;
            this.P2_lives_label.Location = new System.Drawing.Point(378, 421);
            this.P2_lives_label.Name = "P2_lives_label";
            this.P2_lives_label.Size = new System.Drawing.Size(88, 13);
            this.P2_lives_label.TabIndex = 58;
            this.P2_lives_label.Text = "Player 2 Lives : 3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 60;
            this.label8.Text = "Ver: 1.03.04";
            // 
            // Score_Label2
            // 
            this.Score_Label2.AutoSize = true;
            this.Score_Label2.Location = new System.Drawing.Point(402, 439);
            this.Score_Label2.Name = "Score_Label2";
            this.Score_Label2.Size = new System.Drawing.Size(47, 13);
            this.Score_Label2.TabIndex = 61;
            this.Score_Label2.Text = "Score: 0";
            // 
            // Food
            // 
            this.Food.BackColor = System.Drawing.Color.White;
            this.Food.Enabled = false;
            this.Food.InitialImage = null;
            this.Food.Location = new System.Drawing.Point(505, 10);
            this.Food.Name = "Food";
            this.Food.Size = new System.Drawing.Size(14, 14);
            this.Food.TabIndex = 31;
            this.Food.TabStop = false;
            this.Food.WaitOnLoad = true;
            // 
            // Food2
            // 
            this.Food2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Food2.Enabled = false;
            this.Food2.InitialImage = null;
            this.Food2.Location = new System.Drawing.Point(505, 28);
            this.Food2.Name = "Food2";
            this.Food2.Size = new System.Drawing.Size(14, 14);
            this.Food2.TabIndex = 59;
            this.Food2.TabStop = false;
            this.Food2.WaitOnLoad = true;
            // 
            // Snake_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(531, 454);
            this.Controls.Add(this.Score_Label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.P2_lives_label);
            this.Controls.Add(this.Food2);
            this.Controls.Add(this.P1_lives_label);
            this.Controls.Add(this.Player2_Label);
            this.Controls.Add(this.Player1_Label);
            this.Controls.Add(this.Snake_Head2);
            this.Controls.Add(this.Food_Progress);
            this.Controls.Add(this.Food);
            this.Controls.Add(this.Snake_Head);
            this.Controls.Add(this.Start_label);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Level_Label);
            this.Controls.Add(this.Score_Label);
            this.Controls.Add(this.Bord);
            this.MaximizeBox = false;
            this.Name = "Snake_Form";
            this.Text = "Snake Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Snake_Form_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Snake_Form_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.Snake_Head)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Snake_Head2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Food)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Food2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Score_Label;
        private System.Windows.Forms.Label Level_Label;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label Start_label;
        private System.Windows.Forms.PictureBox Snake_Head;
        private System.Windows.Forms.PictureBox Bord;
        private System.Windows.Forms.ProgressBar Food_Progress;
        private System.Windows.Forms.PictureBox Snake_Head2;
        private System.Windows.Forms.Label Player1_Label;
        private System.Windows.Forms.Label Player2_Label;
        private System.Windows.Forms.Label P1_lives_label;
        private System.Windows.Forms.Label P2_lives_label;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Score_Label2;
        private System.Windows.Forms.PictureBox Food;
        private System.Windows.Forms.PictureBox Food2;

    }
}



