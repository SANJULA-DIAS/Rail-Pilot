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
    public partial class UserPWForm : Form
    {
        public UserPWForm()
        {
            InitializeComponent();
        }

        private void UserPWForm_Load(object sender, EventArgs e)
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

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txt_username.Text;
                string password = txt_pwd.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Username and password cannot be blank.");
                }

                // Replace the hardcoded username and password with the actual ones
                string correctUsername = "user01";
                string correctPassword = "Railway123";

                if (username == correctUsername && password == correctPassword)
                {

                    StationMaster obj = new StationMaster();
                    obj.ShowDialog();
                    this.Close();
                }
                else
                {
                    throw new Exception("Invalid username or password.");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
