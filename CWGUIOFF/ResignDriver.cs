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
    public partial class ResignDriver : MetroFramework.Forms.MetroForm
    {
        public ResignDriver()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        private void ResignDriver_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
        }

        private void BTN_DELETE_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Delete from Driver  where Driver_ID = @Driver_ID", con);
                cmd.Parameters.AddWithValue("@Driver_Id", txt_driverid.Text);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                    MessageBox.Show("Driver Deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
