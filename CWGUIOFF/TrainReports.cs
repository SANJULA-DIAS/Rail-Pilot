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
    public partial class TrainReports : Form
    {
        public TrainReports()
        {
            InitializeComponent();
        }

        private void TrainReports_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'TrainDataSet.Trains' table. You can move, or remove it, as needed.
            this.TrainsTableAdapter.Fill(this.TrainDataSet.Trains, comboBox1.Text);

            this.reportViewer1.RefreshReport();
        }
    }
}
