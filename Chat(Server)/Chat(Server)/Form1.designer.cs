namespace Chat_Server_
{
    partial class FormServer
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
            this.labelShow = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelShow
            // 
            this.labelShow.BackColor = System.Drawing.Color.Black;
            this.labelShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShow.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShow.ForeColor = System.Drawing.Color.Lime;
            this.labelShow.Location = new System.Drawing.Point(0, 0);
            this.labelShow.Name = "labelShow";
            this.labelShow.Size = new System.Drawing.Size(284, 261);
            this.labelShow.TabIndex = 0;
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.AutoScrollMinSize = new System.Drawing.Size(10, 10);
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelShow);
            this.MaximumSize = new System.Drawing.Size(300, 600);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FormServer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hosting Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelShow;
    }
}