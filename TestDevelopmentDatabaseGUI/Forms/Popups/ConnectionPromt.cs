using System;
using System.Threading;
using System.Windows.Forms;
using TestDevelopmentDatabaseGUI.Services;

namespace TestDevelopmentDatabaseGUI.Forms.Popups
{
    public partial class ConnectionPromt : Form
    {

        GlobalConfig _globalConfig = GlobalConfig.Instance;
        GlobalObjects _globalObjects = GlobalObjects.Instance;

        //Form DynamicConnectionOptions = null;
        public ConnectionPromt()
        {
            InitializeComponent();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            if (_globalConfig.sqlConfig.AvailableDatabases != null)
            {
                cmbDatabase.DataSource = _globalConfig.sqlConfig.AvailableDatabases;
            }

            if (_globalConfig.sqlConfig.AvailableHosts != null)
            {
                cmbServerName.DataSource = _globalConfig.sqlConfig.AvailableHosts;
            }

            if (_globalConfig.sqlConfig.AvailableHosts != null)
            {
                cmbUsername.DataSource = _globalConfig.sqlConfig.AvailableUsernames;
            }
        }


        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ConnectBtn_Click_1(object sender, EventArgs e)
        {
            
            _globalConfig.sqlConfig.SelectedHost = cmbServerName.Text;
            _globalConfig.sqlConfig.SelectedDatabase = cmbDatabase.Text;
            _globalConfig.sqlConfig.SelectedUser = cmbUsername.Text;
            _globalConfig.sqlConfig.Password = tbxPassword.Text;


            _globalObjects.sqlConnHandler.Connect(cmbServerName.Text, cmbDatabase.Text, cmbUsername.Text, tbxPassword.Text);

            if (_globalObjects.sqlConnHandler.connected)
            {
                AddDatabase(cmbDatabase.Text);
                AddHost(cmbServerName.Text);
                AddUsername(cmbUsername.Text);
                Thread.Sleep(500);
                _globalConfig.Save();

                DialogResult = DialogResult.OK;
            }
        }

        public void AddUsername(string dbName)
        {
            if (!_globalConfig.sqlConfig.AvailableUsernames.Contains(dbName))
            {
                _globalConfig.sqlConfig.AvailableUsernames.Add(dbName);
            }
        }

        public void AddDatabase(string dbName)
        {
            if (!_globalConfig.sqlConfig.AvailableDatabases.Contains(dbName))
            {
                _globalConfig.sqlConfig.AvailableDatabases.Add(dbName);
            }
        }

        public void AddHost(string dbName)
        {
            if (!_globalConfig.sqlConfig.AvailableHosts.Contains(dbName))
            {
                _globalConfig.sqlConfig.AvailableHosts.Add(dbName);
            }
        }
    }
}
