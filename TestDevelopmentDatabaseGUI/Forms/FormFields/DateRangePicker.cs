using System;
using System.Windows.Forms;

namespace TestDevelopmentDatabaseGUI.Forms.FormFields
{
    public partial class DateRangePicker : Form
    {
        public DateRangePicker()
        {
            InitializeComponent();
        }

        private void FromField_ValueChanged(object sender, EventArgs e)
        {
            // If the FromField date is later than the ToField date, set ToField to the same date
            if (this.FromField.Value.Date > this.ToField.Value.Date)
            {
                this.ToField.Value = this.FromField.Value;
            }
        }

        private void ToField_ValueChanged(object sender, EventArgs e)
        {
            if (this.ToField.Value.Date < this.FromField.Value.Date)
            {
                this.FromField.Value = this.ToField.Value;
            }
        }

    }
}
