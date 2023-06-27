using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDevelopmentDatabaseGUI.Forms.FormFields
{
    public partial class ValueRange : Form
    {
        public ValueRange()
        {
            InitializeComponent();
        }

        private void Minimum_ValueChanged(object sender, EventArgs e)
        {
            // If the Minimum value is greater than the Maximum value, set Maximum to the same value
            if (this.Minimum.Value > this.Maximum.Value)
            {
                this.Maximum.Value = this.Minimum.Value;
            }
        }

        private void Maximum_ValueChanged(object sender, EventArgs e)
        {
            // If the Maximum value is less than the Minimum value, set Minimum to the same value
            if (this.Maximum.Value < this.Minimum.Value)
            {
                this.Minimum.Value = this.Maximum.Value;
            }
        }
    }
}
