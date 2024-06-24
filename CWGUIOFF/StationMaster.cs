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
    public partial class StationMaster : Krypton.Toolkit.KryptonForm
    {
        public StationMaster()
        {
            InitializeComponent();
        }

        private void StationMaster_Load(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)  //TO MOVE THE WINDOW
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PassengerReports obj = new PassengerReports();
            obj.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            PassengerDataStationMaster obj = new PassengerDataStationMaster();
            obj.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DriverReport obj = new DriverReport();
            obj.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TrainsMainLoginForm obj = new TrainsMainLoginForm();
            obj.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            DriverReport obj = new DriverReport();
            obj.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            TrainsMainLoginForm obj = new TrainsMainLoginForm();
            obj.ShowDialog();
        }
    }
}
