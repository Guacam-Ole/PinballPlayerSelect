namespace PPS
{
    partial class DisplayText
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
            this.Close = new System.Windows.Forms.Button();
            this.Contents = new System.Windows.Forms.TextBox();
            this.Caption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Close
            // 
            this.Close.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Close.Location = new System.Drawing.Point(356, 409);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(94, 29);
            this.Close.TabIndex = 0;
            this.Close.Text = "OkeyDokey";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Contents
            // 
            this.Contents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Contents.Location = new System.Drawing.Point(12, 109);
            this.Contents.Multiline = true;
            this.Contents.Name = "Contents";
            this.Contents.Size = new System.Drawing.Size(776, 294);
            this.Contents.TabIndex = 1;
            // 
            // Caption
            // 
            this.Caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Caption.Location = new System.Drawing.Point(12, 9);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(776, 97);
            this.Caption.TabIndex = 2;
            this.Caption.Text = "label1";
            // 
            // DisplayText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Caption);
            this.Controls.Add(this.Contents);
            this.Controls.Add(this.Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DisplayText";
            this.Text = "Important stuff:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.TextBox Contents;
        private System.Windows.Forms.Label Caption;
    }
}