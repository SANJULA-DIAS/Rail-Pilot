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
    public partial class DriverLogin : Krypton.Toolkit.KryptonForm
    {
        public DriverLogin()
        {
            InitializeComponent();
        }

        private void DriverLogin_Load(object sender, EventArgs e)
        {

        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            DriverRegister obj = new DriverRegister();
            obj.ShowDialog();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            DriverUpdate obj = new DriverUpdate();
            obj.ShowDialog();
        }

        private void btn_resign_Click(object sender, EventArgs e)
        {
            ResignDriver obj = new ResignDriver();
            obj.ShowDialog();
        }

        private void btn_viewpassengers_Click(object sender, EventArgs e)
        {
            TrainReports obj = new TrainReports();
            obj.ShowDialog();

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
    }
}
