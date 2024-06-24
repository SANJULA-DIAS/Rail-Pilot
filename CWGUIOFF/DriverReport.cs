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
    public partial class DriverReport : Form
    {
        public DriverReport()
        {
            InitializeComponent();
        }

        private void DriverReport_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Validate the input in experiencetxt
            if (!int.TryParse(experiencetxt.Text, out int experience) || experience <= 0)
            {
                MessageBox.Show("Please enter a valid positive numeric value for experience.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without loading the report
            }

            // If the input is valid, proceed to load data into the report
            try
            {
                this.DriverTableAdapter.Fill(this.DriversDataSet.Driver, experience);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void experiencetxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
