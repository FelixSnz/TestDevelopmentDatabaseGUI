namespace TestDevelopmentDatabaseGUI.Forms
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SettingsBtn = new FontAwesome.Sharp.IconButton();
            this.CurrentPanelLbl = new System.Windows.Forms.Label();
            this.DisconnectBtn = new FontAwesome.Sharp.IconButton();
            this.ConnectBtn = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ParentPanel = new System.Windows.Forms.Panel();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.ReportsBtn = new FontAwesome.Sharp.IconButton();
            this.DashboardsBtn = new FontAwesome.Sharp.IconButton();
            this.SequencesBtn = new FontAwesome.Sharp.IconButton();
            this.NewModelBtn = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NewInstrument = new FontAwesome.Sharp.IconButton();
            this.SearchBtn = new FontAwesome.Sharp.IconButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.SettingsBtn);
            this.panel1.Controls.Add(this.DisconnectBtn);
            this.panel1.Controls.Add(this.ConnectBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1060, 53);
            this.panel1.TabIndex = 2;
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.SettingsBtn.FlatAppearance.BorderSize = 0;
            this.SettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsBtn.IconChar = FontAwesome.Sharp.IconChar.Gear;
            this.SettingsBtn.IconColor = System.Drawing.SystemColors.ControlDark;
            this.SettingsBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SettingsBtn.IconSize = 52;
            this.SettingsBtn.Location = new System.Drawing.Point(985, 0);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.Size = new System.Drawing.Size(75, 53);
            this.SettingsBtn.TabIndex = 4;
            this.SettingsBtn.UseVisualStyleBackColor = true;
            // 
            // CurrentPanelLbl
            // 
            this.CurrentPanelLbl.AutoSize = true;
            this.CurrentPanelLbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.CurrentPanelLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentPanelLbl.ForeColor = System.Drawing.Color.Gainsboro;
            this.CurrentPanelLbl.Location = new System.Drawing.Point(251, 0);
            this.CurrentPanelLbl.Name = "CurrentPanelLbl";
            this.CurrentPanelLbl.Size = new System.Drawing.Size(81, 32);
            this.CurrentPanelLbl.TabIndex = 3;
            this.CurrentPanelLbl.Text = "Inicio";
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.DisconnectBtn.FlatAppearance.BorderSize = 0;
            this.DisconnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisconnectBtn.IconChar = FontAwesome.Sharp.IconChar.PlugCircleXmark;
            this.DisconnectBtn.IconColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DisconnectBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.DisconnectBtn.IconSize = 52;
            this.DisconnectBtn.Location = new System.Drawing.Point(75, 0);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(75, 53);
            this.DisconnectBtn.TabIndex = 1;
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.ConnectBtn.FlatAppearance.BorderSize = 0;
            this.ConnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectBtn.IconChar = FontAwesome.Sharp.IconChar.Plug;
            this.ConnectBtn.IconColor = System.Drawing.Color.Gray;
            this.ConnectBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ConnectBtn.IconSize = 42;
            this.ConnectBtn.Location = new System.Drawing.Point(0, 0);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 53);
            this.ConnectBtn.TabIndex = 0;
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1060, 632);
            this.panel2.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ParentPanel);
            this.panel5.Controls.Add(this.MenuPanel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1060, 622);
            this.panel5.TabIndex = 5;
            // 
            // ParentPanel
            // 
            this.ParentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParentPanel.Location = new System.Drawing.Point(227, 0);
            this.ParentPanel.Name = "ParentPanel";
            this.ParentPanel.Size = new System.Drawing.Size(833, 622);
            this.ParentPanel.TabIndex = 2;
            // 
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(85)))), ((int)(((byte)(120)))));
            this.MenuPanel.Controls.Add(this.ReportsBtn);
            this.MenuPanel.Controls.Add(this.DashboardsBtn);
            this.MenuPanel.Controls.Add(this.SequencesBtn);
            this.MenuPanel.Controls.Add(this.NewModelBtn);
            this.MenuPanel.Controls.Add(this.pictureBox1);
            this.MenuPanel.Controls.Add(this.NewInstrument);
            this.MenuPanel.Controls.Add(this.SearchBtn);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.MenuPanel.Size = new System.Drawing.Size(227, 622);
            this.MenuPanel.TabIndex = 1;
            // 
            // ReportsBtn
            // 
            this.ReportsBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.ReportsBtn.FlatAppearance.BorderSize = 0;
            this.ReportsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReportsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportsBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.ReportsBtn.IconChar = FontAwesome.Sharp.IconChar.Scroll;
            this.ReportsBtn.IconColor = System.Drawing.Color.Gainsboro;
            this.ReportsBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ReportsBtn.Location = new System.Drawing.Point(7, 340);
            this.ReportsBtn.Name = "ReportsBtn";
            this.ReportsBtn.Size = new System.Drawing.Size(220, 68);
            this.ReportsBtn.TabIndex = 8;
            this.ReportsBtn.Text = "Reportes";
            this.ReportsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ReportsBtn.UseVisualStyleBackColor = true;
            this.ReportsBtn.Click += new System.EventHandler(this.ReportsBtn_Click);
            // 
            // DashboardsBtn
            // 
            this.DashboardsBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.DashboardsBtn.FlatAppearance.BorderSize = 0;
            this.DashboardsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DashboardsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DashboardsBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.DashboardsBtn.IconChar = FontAwesome.Sharp.IconChar.LineChart;
            this.DashboardsBtn.IconColor = System.Drawing.Color.Gainsboro;
            this.DashboardsBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.DashboardsBtn.Location = new System.Drawing.Point(7, 272);
            this.DashboardsBtn.Name = "DashboardsBtn";
            this.DashboardsBtn.Size = new System.Drawing.Size(220, 68);
            this.DashboardsBtn.TabIndex = 7;
            this.DashboardsBtn.Text = "Dashboards";
            this.DashboardsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DashboardsBtn.UseVisualStyleBackColor = true;
            this.DashboardsBtn.Click += new System.EventHandler(this.DashboardsBtn_Click);
            // 
            // SequencesBtn
            // 
            this.SequencesBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.SequencesBtn.FlatAppearance.BorderSize = 0;
            this.SequencesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SequencesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SequencesBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.SequencesBtn.IconChar = FontAwesome.Sharp.IconChar.List;
            this.SequencesBtn.IconColor = System.Drawing.Color.Gainsboro;
            this.SequencesBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SequencesBtn.Location = new System.Drawing.Point(7, 204);
            this.SequencesBtn.Name = "SequencesBtn";
            this.SequencesBtn.Size = new System.Drawing.Size(220, 68);
            this.SequencesBtn.TabIndex = 6;
            this.SequencesBtn.Text = "Secuencias";
            this.SequencesBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SequencesBtn.UseVisualStyleBackColor = true;
            this.SequencesBtn.Click += new System.EventHandler(this.SequencesBtn_Click);
            // 
            // NewModelBtn
            // 
            this.NewModelBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.NewModelBtn.FlatAppearance.BorderSize = 0;
            this.NewModelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewModelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewModelBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.NewModelBtn.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.NewModelBtn.IconColor = System.Drawing.Color.Gainsboro;
            this.NewModelBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.NewModelBtn.Location = new System.Drawing.Point(7, 136);
            this.NewModelBtn.Name = "NewModelBtn";
            this.NewModelBtn.Size = new System.Drawing.Size(220, 68);
            this.NewModelBtn.TabIndex = 5;
            this.NewModelBtn.Text = "Modelos";
            this.NewModelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewModelBtn.UseVisualStyleBackColor = true;
            this.NewModelBtn.Click += new System.EventHandler(this.NewModelBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TestDevelopmentDatabaseGUI.Properties.Resources.Johnson_Controls_Logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(7, 486);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 136);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // NewInstrument
            // 
            this.NewInstrument.Dock = System.Windows.Forms.DockStyle.Top;
            this.NewInstrument.FlatAppearance.BorderSize = 0;
            this.NewInstrument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewInstrument.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewInstrument.ForeColor = System.Drawing.Color.Gainsboro;
            this.NewInstrument.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.NewInstrument.IconColor = System.Drawing.Color.Gainsboro;
            this.NewInstrument.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.NewInstrument.Location = new System.Drawing.Point(7, 68);
            this.NewInstrument.Name = "NewInstrument";
            this.NewInstrument.Size = new System.Drawing.Size(220, 68);
            this.NewInstrument.TabIndex = 1;
            this.NewInstrument.Text = "Instrumentos";
            this.NewInstrument.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewInstrument.UseVisualStyleBackColor = true;
            this.NewInstrument.Click += new System.EventHandler(this.NewInstrument_Click);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchBtn.FlatAppearance.BorderSize = 0;
            this.SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.SearchBtn.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.SearchBtn.IconColor = System.Drawing.Color.Gainsboro;
            this.SearchBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SearchBtn.Location = new System.Drawing.Point(7, 0);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(220, 68);
            this.SearchBtn.TabIndex = 0;
            this.SearchBtn.Text = "Buscar";
            this.SearchBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1060, 10);
            this.panel4.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.CurrentPanelLbl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(150, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(332, 53);
            this.panel3.TabIndex = 6;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblDataSource);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(482, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(503, 53);
            this.panel6.TabIndex = 7;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(138, 53);
            this.panel7.TabIndex = 6;
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDataSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataSource.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDataSource.Location = new System.Drawing.Point(138, 0);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(112, 32);
            this.lblDataSource.TabIndex = 7;
            this.lblDataSource.Text = "Source:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 685);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.MenuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton ConnectBtn;
        private FontAwesome.Sharp.IconButton DisconnectBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel MenuPanel;
        private FontAwesome.Sharp.IconButton SearchBtn;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel ParentPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label CurrentPanelLbl;
        private FontAwesome.Sharp.IconButton ReportsBtn;
        private FontAwesome.Sharp.IconButton DashboardsBtn;
        private FontAwesome.Sharp.IconButton SequencesBtn;
        private FontAwesome.Sharp.IconButton NewModelBtn;
        private FontAwesome.Sharp.IconButton NewInstrument;
        private FontAwesome.Sharp.IconButton SettingsBtn;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblDataSource;
        private System.Windows.Forms.Panel panel7;
    }
}

