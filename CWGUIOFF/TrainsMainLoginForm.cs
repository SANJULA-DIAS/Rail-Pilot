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
    public partial class TrainsMainLoginForm : Form
    {
        public TrainsMainLoginForm()
        {
            InitializeComponent();
        }

        private void TrainsMainLoginForm_Load(object sender, EventArgs e)
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

            private void btn_booktrain_Click(object sender, EventArgs e)
        {
            AddTrains obj = new AddTrains();
            obj.ShowDialog();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            UpdateTrain obj = new UpdateTrain();
            obj.ShowDialog();

        }

        private void btn_trainreports_Click(object sender, EventArgs e)
        {
            TrainReports obj = new TrainReports();
            obj.ShowDialog();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            RemoveTrain obj = new RemoveTrain();
            obj.ShowDialog();
        }
    }
}
