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
    public partial class MainLoginForm : Krypton.Toolkit.KryptonForm
    {
        public MainLoginForm()
        {
            InitializeComponent();
        }

        private void MainLoginForm_Load(object sender, EventArgs e)
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

        private void btn_passenger_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.ShowDialog();
        }
 

        private void lbl_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            DriverLogin obj = new DriverLogin();
            obj.ShowDialog();
        }

        private void guna2TileButton2_Click_1(object sender, EventArgs e)
        {
            UserPWForm obj = new UserPWForm();
            obj.ShowDialog();
        }
    }
}
