using FontAwesome.Sharp;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TestDevelopmentDatabaseGUI.Forms.Popups;
using TestDevelopmentDatabaseGUI.Services;

namespace TestDevelopmentDatabaseGUI.Forms
{
    public partial class Main : Form
    {

        //private IconButton CurrentBtn;
        //private Panel LeftBorderBtn;
        private Form CurrentChildForm;
        private IconButton CurrentBtn;
        private Panel LeftBorderBtn { get;set;}
        private GlobalObjects _globalObjects = GlobalObjects.Instance;
        private GlobalConfig _globalConfig = GlobalConfig.Instance;  

        public static Main Instance { get; private set; }
        public Main()
        {
            if (Instance != null)
                throw new Exception("Only one instance of MainForm allowed!");
            Instance = this;

            InitializeComponent();
            LeftBorderBtn = new Panel();
            LeftBorderBtn.Size = new Size(7, 68);
            MenuPanel.Controls.Add(LeftBorderBtn);
            this.Size = new Size(1200, 900);
            this.StartPosition = FormStartPosition.CenterScreen;

            _globalObjects.sqlConnHandler = new SqlConnHandler();
            UpdateConnectionStatus();


            ConnectBtn_Click(this, new EventArgs());

        }

        private void UpdateConnectionStatus()
        {
            if (_globalObjects.sqlConnHandler != null)
            {
                if (_globalObjects.sqlConnHandler.connected)
                {
                    ConnectBtn.IconColor = Color.ForestGreen;
                    DisconnectBtn.Enabled = true;
                    DisconnectBtn.IconColor = Color.LightGray;
                }
                else
                {
                    ConnectBtn.IconColor = Color.LightGray;
                    DisconnectBtn.Enabled = false;
                    ConnectBtn.Enabled = true;
                    DisconnectBtn.IconColor = Color.Gray;
                }
            }
        }

        public void CloseCurrentChildForm()
        {
            if (CurrentChildForm != null)
            {
                (CurrentChildForm as Form).Close();
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            ConnectionPromt connectionPromt = new ConnectionPromt();

            connectionPromt.StartPosition = FormStartPosition.CenterScreen;

            if (connectionPromt.ShowDialog() == DialogResult.OK)
            {
                UpdateConnectionStatus();
                DatabaseConfig dbConfigPromt = new DatabaseConfig();

                dbConfigPromt.StartPosition = FormStartPosition.CenterScreen;

                dbConfigPromt.ShowDialog();
            }
            else
            {

            }
        }


        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            if (_globalObjects.sqlConnHandler.connected)
            {
                if (_globalObjects.sqlConnHandler.connState == ConnectionState.Open)
                {
                    _globalObjects.sqlConnHandler.Disconnect();
                    UpdateConnectionStatus();
                }
            }
            else
            {

            }
        }

        public void SetButtonsState(bool state)
        {
            SearchBtn.Enabled = state;
            NewInstrument.Enabled = state;
            NewModelBtn.Enabled = state;
            SequencesBtn.Enabled = state;
            DashboardsBtn.Enabled = state;
            ReportsBtn.Enabled = state;
            SettingsBtn.Enabled = state;
        }



        public void UpdateDataSourceLabel()
        {

            lblDataSource.Text = $"{_globalConfig.sqlConfig.SelectedDatabase}:{_globalConfig.sqlConfig.TableName}"; 
            
        }

        public void ClearDataSourceLabel()
        {
            lblDataSource.Text = string.Empty ;
        }


        private void OpenChildForm(Object sender, Func<Form> formFactory)
        {
            if (!_globalObjects.sqlConnHandler.connected)
            {
                return;
            }

            //open only form
            if (CurrentChildForm != null)
            {
                (CurrentChildForm as Form).Close();
            }
            CurrentChildForm = formFactory();

            Thread.Sleep(500);

            if (!CurrentChildForm.IsDisposed)
            {
                CurrentChildForm.TopLevel = false;
                CurrentChildForm.FormBorderStyle = FormBorderStyle.None;
                CurrentChildForm.Dock = DockStyle.Fill;
                ParentPanel.Controls.Add(CurrentChildForm);
                ParentPanel.Tag = CurrentChildForm;
                CurrentChildForm.BringToFront();
                CurrentChildForm.Show();

                CurrentPanelLbl.Text = CurrentChildForm.Text;

                HighligthBtn(sender, Color.LightBlue);
            }
            
        }



        private void HighligthBtn(object senderBtn, Color color)
        {

            if (senderBtn != null)
            {
                UnHighligthCurrentBtn();


                //current button
                CurrentBtn = (IconButton)senderBtn;
                CurrentBtn.BackColor = Color.FromArgb(75, 95, 140);
                CurrentBtn.ForeColor = color;
                CurrentBtn.TextAlign = ContentAlignment.MiddleCenter;
                CurrentBtn.IconColor = color;
                CurrentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                CurrentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //left border on current button
                LeftBorderBtn.BackColor = color;
                LeftBorderBtn.Location = new Point(0, CurrentBtn.Location.Y);
                LeftBorderBtn.Visible = true;
                LeftBorderBtn.BringToFront();

            }
        }

        public void UnHighligthCurrentBtn()
        {
            if (CurrentBtn != null)
            {
                CurrentBtn.BackColor = Color.FromArgb(65, 85, 120);
                CurrentBtn.ForeColor = Color.Gainsboro;
                CurrentBtn.TextAlign = ContentAlignment.MiddleLeft;
                CurrentBtn.IconColor = Color.Gainsboro;
                CurrentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                CurrentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                LeftBorderBtn.Visible = false;
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(sender, () => new SearchPanel());
        }

        private void NewInstrument_Click(object sender, EventArgs e)
        {
            OpenChildForm(sender, () => new NewInstrumentPanel());
        }

        private void NewModelBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(sender, () => new NewModelPanel());
        }

        private void SequencesBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(sender, () => new SequenceEditorPanel());
        }

        private void DashboardsBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(sender, () => new DashboardsPanel());
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(sender, () => new ReportsPanel());
        }
    }
}
