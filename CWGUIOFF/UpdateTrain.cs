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
    public partial class UpdateTrain : Form
    {
        public UpdateTrain()
        {
            InitializeComponent();
        }

        SqlCommand cmd;
        SqlConnection con;

        private void UpdateTrain_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
            txt_trainid.ReadOnly = true;
            txt_usage.Visible = false;
            guna2PictureBox6.Visible = false;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                // Date check: Ensure the selected date is not in the future
                if (mfddatetimepicker.Value.Date > DateTime.Now.Date)
                {
                    throw new ArgumentException("Manufacturing date cannot be a future date.");
                }

                // Validate that txt_usage contains a numeric value
                if (!double.TryParse(txt_usage.Text, out double usage))
                {
                    throw new ArgumentException("Usage years must be a numeric value.");
                }

                // Validate that txt_weight contains a numeric value
                if (!double.TryParse(txt_weight.Text, out double weight))
                {
                    throw new ArgumentException("Weight must be a numeric value.");
                }

                con.Open();
                cmd = new SqlCommand("UPDATE Trains SET Train_Name = @Train_Name, Train_Condi = @Train_Condi, Train_UsageYrs = @Train_UsageYrs, Train_MFD = @Train_MFD, Train_Weight = @Train_Weight WHERE Train_Id = @Train_Id", con);
                cmd.Parameters.AddWithValue("@Train_Name", txt_trainname.Text);
                cmd.Parameters.AddWithValue("@Train_Condi", conditioncombobox.Text);
                cmd.Parameters.AddWithValue("@Train_UsageYrs", txt_usage.Text);
                cmd.Parameters.AddWithValue("@Train_MFD", mfddatetimepicker.Value);
                cmd.Parameters.AddWithValue("@Train_Weight", txt_weight.Text);
                cmd.Parameters.AddWithValue("@Train_Id", txt_trainid.Text);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Train Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Something's wrong, please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void conditioncombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conditioncombobox.SelectedIndex == 0)
            {
                txt_usage.Visible = false;
                guna2PictureBox6.Visible = false;
                txt_usage.Text = "0"; // Optionally, set the value to "0" when hidden
            }
            else if (conditioncombobox.SelectedIndex == 1)
            {
                txt_usage.Visible = true;
                guna2PictureBox6.Visible = true;
                txt_usage.ReadOnly = false; // Optionally, allow the user to input values when shown
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            txt_trainname.Clear();
            txt_usage.Clear();
            txt_weight.Clear();
            conditioncombobox.ResetText();
            txt_trainid.Clear();
            mfddatetimepicker.ResetText();
        }
    }
}
