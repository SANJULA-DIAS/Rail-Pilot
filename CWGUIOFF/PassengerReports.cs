using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CWGUIOFF
{
    public partial class PassengerReports : Form
    {
        public PassengerReports()
        {
            InitializeComponent();
        }

        private void PassengerReports_Load(object sender, EventArgs e)
        {
           
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void tocombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSetPassengers.Passenger' table. You can move, or remove it, as needed.
            this.PassengerTableAdapter.Fill(this.DataSetPassengers.Passenger, fromcombobox.Text, tocombobox.Text);

            this.reportViewer1.RefreshReport();
        }

        private void fromcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tocombobox.ResetText();

            // Get the selected index of the first ComboBox
            int selectedIndex = fromcombobox.SelectedIndex;

            // Hide the corresponding item in the second ComboBox
            for (int i = 0; i < fromcombobox.Items.Count; i++)
            {
                if (i != selectedIndex)
                {
                    tocombobox.Items.Add(fromcombobox.Items[i]);
                }
            }
        }
    }
}
