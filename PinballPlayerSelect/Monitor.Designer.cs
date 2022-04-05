namespace PPS
{
    partial class Monitor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitor));
            this.PlayerNum = new System.Windows.Forms.PictureBox();
            this.coordsInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerNum)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayerNum
            // 
            this.PlayerNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlayerNum.BackColor = System.Drawing.Color.Transparent;
            this.PlayerNum.Location = new System.Drawing.Point(249, 72);
            this.PlayerNum.Name = "PlayerNum";
            this.PlayerNum.Size = new System.Drawing.Size(125, 62);
            this.PlayerNum.TabIndex = 1;
            this.PlayerNum.TabStop = false;
            this.PlayerNum.Tag = "Player";
            // 
            // coordsInfo
            // 
            this.coordsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coordsInfo.Location = new System.Drawing.Point(86, 109);
            this.coordsInfo.Name = "coordsInfo";
            this.coordsInfo.Size = new System.Drawing.Size(601, 272);
            this.coordsInfo.TabIndex = 2;
            this.coordsInfo.Text = "label1";
            this.coordsInfo.Visible = false;
            // 
            // Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.coordsInfo);
            this.Controls.Add(this.PlayerNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Monitor";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Monitor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Monitor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.PlayerNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox PlayerNum;
        private System.Windows.Forms.Label coordsInfo;
    }
}
