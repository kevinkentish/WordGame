using System;

namespace Chat_Client_
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.label1 = new System.Windows.Forms.Label();
            this.Player1Name = new System.Windows.Forms.Label();
            this.Player2Name = new System.Windows.Forms.Label();
            this.Player1Score = new System.Windows.Forms.Label();
            this.Player2Score = new System.Windows.Forms.Label();
            this.Results = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 22F);
            this.label1.Location = new System.Drawing.Point(339, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "LEADERBOARD";
            // 
            // Player1Name
            // 
            this.Player1Name.AutoSize = true;
            this.Player1Name.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player1Name.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1Name.Location = new System.Drawing.Point(277, 177);
            this.Player1Name.MinimumSize = new System.Drawing.Size(200, 50);
            this.Player1Name.Name = "Player1Name";
            this.Player1Name.Size = new System.Drawing.Size(200, 50);
            this.Player1Name.TabIndex = 1;
            this.Player1Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Player2Name
            // 
            this.Player2Name.AutoSize = true;
            this.Player2Name.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player2Name.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2Name.Location = new System.Drawing.Point(457, 177);
            this.Player2Name.MinimumSize = new System.Drawing.Size(200, 50);
            this.Player2Name.Name = "Player2Name";
            this.Player2Name.Size = new System.Drawing.Size(200, 50);
            this.Player2Name.TabIndex = 2;
            this.Player2Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Player2Name.Click += new System.EventHandler(this.Player2Name_Click);
            // 
            // Player1Score
            // 
            this.Player1Score.AutoSize = true;
            this.Player1Score.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player1Score.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1Score.Location = new System.Drawing.Point(318, 243);
            this.Player1Score.Name = "Player1Score";
            this.Player1Score.Size = new System.Drawing.Size(18, 18);
            this.Player1Score.TabIndex = 3;
            this.Player1Score.Text = "0";
            // 
            // Player2Score
            // 
            this.Player2Score.AutoSize = true;
            this.Player2Score.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player2Score.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2Score.Location = new System.Drawing.Point(549, 243);
            this.Player2Score.Name = "Player2Score";
            this.Player2Score.Size = new System.Drawing.Size(18, 18);
            this.Player2Score.TabIndex = 4;
            this.Player2Score.Text = "0";
            // 
            // Results
            // 
            this.Results.AutoSize = true;
            this.Results.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Results.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Results.Location = new System.Drawing.Point(-1, 299);
            this.Results.MinimumSize = new System.Drawing.Size(900, 0);
            this.Results.Name = "Results";
            this.Results.Size = new System.Drawing.Size(900, 28);
            this.Results.TabIndex = 5;
            this.Results.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(404, 357);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "New Game";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(896, 490);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Results);
            this.Controls.Add(this.Player2Score);
            this.Controls.Add(this.Player1Score);
            this.Controls.Add(this.Player2Name);
            this.Controls.Add(this.Player1Name);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Goldenrod;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leaderboard";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Player1Name;
        private System.Windows.Forms.Label Player2Name;
        private System.Windows.Forms.Label Player1Score;
        private System.Windows.Forms.Label Player2Score;
        private System.Windows.Forms.Label Results;
        private System.Windows.Forms.Button button1;
    }
}