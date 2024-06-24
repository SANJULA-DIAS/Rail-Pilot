using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CWGUIOFF
{
    public partial class RemoveTrain : Form
    {
        public RemoveTrain()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RemoveTrain_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");

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

        private void btn_addtrain_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Delete from Trains where Train_ID = @Train_ID", con);
                cmd.Parameters.AddWithValue("@Train_ID", txt_trainid.Text);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                    MessageBox.Show("Train Removed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (i == 0)
                    MessageBox.Show("Driver is not registered", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data cannot be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
