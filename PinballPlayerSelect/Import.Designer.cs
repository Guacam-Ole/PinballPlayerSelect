namespace PPS
{
    partial class Import
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Import));
            label1 = new System.Windows.Forms.Label();
            pbxLabel = new System.Windows.Forms.Label();
            pbxInput = new System.Windows.Forms.TextBox();
            pbxBrowse = new System.Windows.Forms.Button();
            VerifyExePath = new System.Windows.Forms.Button();
            Screen = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            label18 = new System.Windows.Forms.Label();
            textBox9 = new System.Windows.Forms.TextBox();
            label26 = new System.Windows.Forms.Label();
            textBox14 = new System.Windows.Forms.TextBox();
            label27 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            textBox10 = new System.Windows.Forms.TextBox();
            label13 = new System.Windows.Forms.Label();
            textBox11 = new System.Windows.Forms.TextBox();
            label14 = new System.Windows.Forms.Label();
            textBox12 = new System.Windows.Forms.TextBox();
            label15 = new System.Windows.Forms.Label();
            textBox13 = new System.Windows.Forms.TextBox();
            label16 = new System.Windows.Forms.Label();
            dmdScreen = new System.Windows.Forms.ComboBox();
            label17 = new System.Windows.Forms.Label();
            imageList1 = new System.Windows.Forms.ImageList(components);
            Overlays = new System.Windows.Forms.GroupBox();
            groupBox8 = new System.Windows.Forms.GroupBox();
            dmdPreview = new System.Windows.Forms.PictureBox();
            DmdHeightLabel = new System.Windows.Forms.Label();
            DmdHeight = new System.Windows.Forms.TrackBar();
            label37 = new System.Windows.Forms.Label();
            DmdWidthLabel = new System.Windows.Forms.Label();
            DmdWidth = new System.Windows.Forms.TrackBar();
            label39 = new System.Windows.Forms.Label();
            dmdOverlay = new System.Windows.Forms.ComboBox();
            label40 = new System.Windows.Forms.Label();
            WriteConfig = new System.Windows.Forms.Button();
            Screen.SuspendLayout();
            groupBox4.SuspendLayout();
            Overlays.SuspendLayout();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dmdPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DmdHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DmdWidth).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 18);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(467, 15);
            label1.TabIndex = 0;
            label1.Text = "Import Configuration from PinballX. Please select the location of your PinballX.Exe-File:";
            // 
            // pbxLabel
            // 
            pbxLabel.AutoSize = true;
            pbxLabel.Location = new System.Drawing.Point(20, 49);
            pbxLabel.Name = "pbxLabel";
            pbxLabel.Size = new System.Drawing.Size(72, 15);
            pbxLabel.TabIndex = 1;
            pbxLabel.Text = "PinballX.EXE";
            // 
            // pbxInput
            // 
            pbxInput.Location = new System.Drawing.Point(105, 46);
            pbxInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            pbxInput.Name = "pbxInput";
            pbxInput.Size = new System.Drawing.Size(776, 23);
            pbxInput.TabIndex = 2;
            // 
            // pbxBrowse
            // 
            pbxBrowse.Location = new System.Drawing.Point(886, 45);
            pbxBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            pbxBrowse.Name = "pbxBrowse";
            pbxBrowse.Size = new System.Drawing.Size(82, 22);
            pbxBrowse.TabIndex = 3;
            pbxBrowse.Text = "Browse";
            pbxBrowse.UseVisualStyleBackColor = true;
            pbxBrowse.Click += pbxBrowse_Click;
            // 
            // VerifyExePath
            // 
            VerifyExePath.Enabled = false;
            VerifyExePath.Location = new System.Drawing.Point(20, 78);
            VerifyExePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            VerifyExePath.Name = "VerifyExePath";
            VerifyExePath.Size = new System.Drawing.Size(948, 22);
            VerifyExePath.TabIndex = 4;
            VerifyExePath.Text = "Read Settings";
            VerifyExePath.UseVisualStyleBackColor = true;
            VerifyExePath.Click += VerifyExePath_Click;
            // 
            // Screens
            // 
            Screen.Controls.Add(groupBox4);
            Screen.Enabled = false;
            Screen.Location = new System.Drawing.Point(20, 119);
            Screen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Screen.Name = "Screens";
            Screen.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Screen.Size = new System.Drawing.Size(566, 154);
            Screen.TabIndex = 6;
            Screen.TabStop = false;
            Screen.Tag = "Screen";
            Screen.Text = "Screen";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label18);
            groupBox4.Controls.Add(textBox9);
            groupBox4.Controls.Add(label26);
            groupBox4.Controls.Add(textBox14);
            groupBox4.Controls.Add(label27);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(textBox10);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(textBox11);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(textBox12);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(textBox13);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(dmdScreen);
            groupBox4.Controls.Add(label17);
            groupBox4.Location = new System.Drawing.Point(6, 20);
            groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox4.Size = new System.Drawing.Size(553, 116);
            groupBox4.TabIndex = 11;
            groupBox4.TabStop = false;
            groupBox4.Tag = "DMD";
            groupBox4.Text = "DMD";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(70, 82);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(55, 15);
            label18.TabIndex = 20;
            label18.Text = "Rotation:";
            // 
            // textBox9
            // 
            textBox9.Location = new System.Drawing.Point(350, 77);
            textBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox9.Name = "textBox9";
            textBox9.Size = new System.Drawing.Size(70, 23);
            textBox9.TabIndex = 19;
            textBox9.Tag = "OverlayRotate";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new System.Drawing.Point(290, 80);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(50, 15);
            label26.TabIndex = 18;
            label26.Text = "Overlay:";
            // 
            // textBox14
            // 
            textBox14.Location = new System.Drawing.Point(194, 80);
            textBox14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox14.Name = "textBox14";
            textBox14.Size = new System.Drawing.Size(70, 23);
            textBox14.TabIndex = 17;
            textBox14.Tag = "ScreenRotate";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new System.Drawing.Point(140, 82);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(45, 15);
            label27.TabIndex = 16;
            label27.Text = "Screen:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(450, 49);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(90, 15);
            label12.TabIndex = 10;
            label12.Text = "(*) 0=Fullscreen";
            // 
            // textBox10
            // 
            textBox10.Location = new System.Drawing.Point(350, 46);
            textBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox10.Name = "textBox10";
            textBox10.Size = new System.Drawing.Size(70, 23);
            textBox10.TabIndex = 9;
            textBox10.Tag = "ScreenHeight";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(281, 49);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(59, 15);
            label13.TabIndex = 8;
            label13.Text = "Height(*):";
            // 
            // textBox11
            // 
            textBox11.Location = new System.Drawing.Point(194, 46);
            textBox11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox11.Name = "textBox11";
            textBox11.Size = new System.Drawing.Size(70, 23);
            textBox11.TabIndex = 7;
            textBox11.Tag = "ScreenWidth";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(130, 49);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(55, 15);
            label14.TabIndex = 6;
            label14.Text = "Width(*):";
            // 
            // textBox12
            // 
            textBox12.Location = new System.Drawing.Point(350, 14);
            textBox12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox12.Name = "textBox12";
            textBox12.Size = new System.Drawing.Size(70, 23);
            textBox12.TabIndex = 5;
            textBox12.Tag = "ScreenY";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(327, 16);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(17, 15);
            label15.TabIndex = 4;
            label15.Text = "Y:";
            // 
            // textBox13
            // 
            textBox13.Location = new System.Drawing.Point(194, 14);
            textBox13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox13.Name = "textBox13";
            textBox13.Size = new System.Drawing.Size(70, 23);
            textBox13.TabIndex = 3;
            textBox13.Tag = "ScreenX";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(171, 17);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(17, 15);
            label16.TabIndex = 2;
            label16.Text = "X:";
            // 
            // dmdScreen
            // 
            dmdScreen.DisplayMember = "KEy";
            dmdScreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dmdScreen.FormattingEnabled = true;
            dmdScreen.Location = new System.Drawing.Point(71, 15);
            dmdScreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            dmdScreen.Name = "dmdScreen";
            dmdScreen.Size = new System.Drawing.Size(60, 23);
            dmdScreen.TabIndex = 1;
            dmdScreen.Tag = "ScreenId";
            dmdScreen.ValueMember = "Key";
            dmdScreen.SelectedIndexChanged += ScreenCombo_SelectedIndexChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(5, 17);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(55, 15);
            label17.TabIndex = 0;
            label17.Text = "ScreenId:";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList1.ImageSize = new System.Drawing.Size(16, 16);
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Overlays
            // 
            Overlays.Controls.Add(groupBox8);
            Overlays.Enabled = false;
            Overlays.Location = new System.Drawing.Point(594, 119);
            Overlays.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Overlays.Name = "Overlays";
            Overlays.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Overlays.Size = new System.Drawing.Size(528, 154);
            Overlays.TabIndex = 7;
            Overlays.TabStop = false;
            Overlays.Tag = "Overlay";
            Overlays.Text = "Overlay";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(dmdPreview);
            groupBox8.Controls.Add(DmdHeightLabel);
            groupBox8.Controls.Add(DmdHeight);
            groupBox8.Controls.Add(label37);
            groupBox8.Controls.Add(DmdWidthLabel);
            groupBox8.Controls.Add(DmdWidth);
            groupBox8.Controls.Add(label39);
            groupBox8.Controls.Add(dmdOverlay);
            groupBox8.Controls.Add(label40);
            groupBox8.Location = new System.Drawing.Point(12, 20);
            groupBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox8.Size = new System.Drawing.Size(516, 116);
            groupBox8.TabIndex = 10;
            groupBox8.TabStop = false;
            groupBox8.Tag = "DMD";
            groupBox8.Text = "DMD";
            // 
            // dmdPreview
            // 
            dmdPreview.Location = new System.Drawing.Point(278, 19);
            dmdPreview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            dmdPreview.Name = "dmdPreview";
            dmdPreview.Size = new System.Drawing.Size(233, 79);
            dmdPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            dmdPreview.TabIndex = 9;
            dmdPreview.TabStop = false;
            dmdPreview.Tag = "OverlayImage";
            // 
            // DmdHeightLabel
            // 
            DmdHeightLabel.AutoSize = true;
            DmdHeightLabel.Location = new System.Drawing.Point(230, 73);
            DmdHeightLabel.Name = "DmdHeightLabel";
            DmdHeightLabel.Size = new System.Drawing.Size(38, 15);
            DmdHeightLabel.TabIndex = 8;
            DmdHeightLabel.Tag = "HeightLabel";
            DmdHeightLabel.Text = "100 %";
            // 
            // DmdHeight
            // 
            DmdHeight.Location = new System.Drawing.Point(54, 71);
            DmdHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            DmdHeight.Maximum = 100;
            DmdHeight.Name = "DmdHeight";
            DmdHeight.Size = new System.Drawing.Size(171, 45);
            DmdHeight.TabIndex = 7;
            DmdHeight.Tag = "OverlayHeight";
            DmdHeight.TickFrequency = 10;
            DmdHeight.TickStyle = System.Windows.Forms.TickStyle.None;
            DmdHeight.Value = 100;
            DmdHeight.Scroll += OverlayHeight_Scroll;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new System.Drawing.Point(5, 71);
            label37.Name = "label37";
            label37.Size = new System.Drawing.Size(46, 15);
            label37.TabIndex = 6;
            label37.Text = "Height:";
            // 
            // DmdWidthLabel
            // 
            DmdWidthLabel.AutoSize = true;
            DmdWidthLabel.Location = new System.Drawing.Point(230, 46);
            DmdWidthLabel.Name = "DmdWidthLabel";
            DmdWidthLabel.Size = new System.Drawing.Size(38, 15);
            DmdWidthLabel.TabIndex = 5;
            DmdWidthLabel.Tag = "WidthLabel";
            DmdWidthLabel.Text = "100 %";
            // 
            // DmdWidth
            // 
            DmdWidth.Location = new System.Drawing.Point(54, 46);
            DmdWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            DmdWidth.Maximum = 100;
            DmdWidth.Name = "DmdWidth";
            DmdWidth.Size = new System.Drawing.Size(171, 45);
            DmdWidth.TabIndex = 4;
            DmdWidth.Tag = "OverlayWidth";
            DmdWidth.TickFrequency = 10;
            DmdWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            DmdWidth.Value = 100;
            DmdWidth.Scroll += OverlayWidth_Scroll;
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Location = new System.Drawing.Point(9, 46);
            label39.Name = "label39";
            label39.Size = new System.Drawing.Size(42, 15);
            label39.TabIndex = 3;
            label39.Text = "Width:";
            // 
            // dmdOverlay
            // 
            dmdOverlay.DisplayMember = "Value";
            dmdOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dmdOverlay.FormattingEnabled = true;
            dmdOverlay.Location = new System.Drawing.Point(54, 19);
            dmdOverlay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            dmdOverlay.Name = "dmdOverlay";
            dmdOverlay.Size = new System.Drawing.Size(219, 23);
            dmdOverlay.TabIndex = 2;
            dmdOverlay.Tag = "OverlayStyle";
            dmdOverlay.ValueMember = "Key";
            dmdOverlay.SelectedIndexChanged += BackGlassOverlay_SelectedIndexChanged;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Location = new System.Drawing.Point(16, 21);
            label40.Name = "label40";
            label40.Size = new System.Drawing.Size(35, 15);
            label40.TabIndex = 1;
            label40.Text = "Style:";
            // 
            // WriteConfig
            // 
            WriteConfig.Enabled = false;
            WriteConfig.Location = new System.Drawing.Point(991, 21);
            WriteConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            WriteConfig.Name = "WriteConfig";
            WriteConfig.Size = new System.Drawing.Size(125, 94);
            WriteConfig.TabIndex = 8;
            WriteConfig.Text = "Write Config";
            WriteConfig.UseVisualStyleBackColor = true;
            WriteConfig.Click += WriteConfig_Click;
            // 
            // Import
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1134, 282);
            Controls.Add(WriteConfig);
            Controls.Add(Overlays);
            Controls.Add(Screen);
            Controls.Add(VerifyExePath);
            Controls.Add(pbxBrowse);
            Controls.Add(pbxInput);
            Controls.Add(pbxLabel);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Import";
            Text = "First Time Configuration";
            Screen.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            Overlays.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dmdPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)DmdHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)DmdWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label pbxLabel;
        private System.Windows.Forms.TextBox pbxInput;
        private System.Windows.Forms.Button pbxBrowse;
        private System.Windows.Forms.Button VerifyExePath;
        private System.Windows.Forms.GroupBox Screen;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox dmdScreen;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox Overlays;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.PictureBox dmdPreview;
        private System.Windows.Forms.Label DmdHeightLabel;
        private System.Windows.Forms.TrackBar DmdHeight;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label DmdWidthLabel;
        private System.Windows.Forms.TrackBar DmdWidth;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ComboBox dmdOverlay;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button WriteConfig;
    }
}