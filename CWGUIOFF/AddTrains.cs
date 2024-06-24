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
using System.Text.RegularExpressions;

namespace CWGUIOFF
{
    public partial class AddTrains : MetroFramework.Forms.MetroForm
    {
        SqlConnection con;
        SqlCommand cmd;

        public AddTrains()
        {
            InitializeComponent();
        }

        private void AddTrains_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
            txt_trainid.ReadOnly = true;
            txt_trainid.Text = GetNextTrainID();
            txt_usage.Visible = false;
            guna2PictureBox6.Visible = false;
        }

        private void btn_addtrain_Click(object sender, EventArgs e)
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
                cmd = new SqlCommand("INSERT INTO Trains VALUES (@Train_Id, @Train_Name, @Train_Condi, @Train_UsageYrs, @Train_MFD, @Train_Weight)", con);
                cmd.Parameters.AddWithValue("@Train_Id", txt_trainid.Text);
                cmd.Parameters.AddWithValue("@Train_Name", txt_trainname.Text);
                cmd.Parameters.AddWithValue("@Train_Condi", conditioncombobox.Text);
                cmd.Parameters.AddWithValue("@Train_UsageYrs", txt_usage.Text);
                cmd.Parameters.AddWithValue("@Train_MFD", mfddatetimepicker.Value);
                cmd.Parameters.AddWithValue("@Train_Weight", txt_weight.Text);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Train Added Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    int previousTrainID;
                    bool success = int.TryParse(txt_trainid.Text.Substring(1), out previousTrainID);

                    // If the previous train ID could be parsed successfully, increment it
                    if (success)
                    {
                        int nextTrainID = previousTrainID + 1;
                        txt_trainid.Text = "T" + nextTrainID;

                        txt_trainname.Clear();
                        txt_usage.Clear();
                        txt_weight.Clear();
                        conditioncombobox.ResetText();
                    }
                }
                else
                {
                    MessageBox.Show("Something's wrong, please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private string GetNextTrainID()
        {
            string nextTrainID = string.Empty;

            using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True"))
            {
                con.Open();

                // Retrieve the maximum ticket ID from the Passenger table
                SqlCommand cmd = new SqlCommand("SELECT MAX(Train_ID) FROM Trains", con);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string currentTrainID = result.ToString();
                    string numericPart = currentTrainID.Substring(1); // Extract the numeric part of the ID
                    int numericValue;

                    if (int.TryParse(numericPart, out numericValue))
                    {
                        // If a valid numeric part exists, increment it by 1 and concatenate with the prefix
                        nextTrainID = "T" + (numericValue + 1);
                    }
                    else
                    {
                        // Handle the case when the numeric part cannot be parsed
                        throw new InvalidOperationException("Invalid ticket ID format in the database.");
                    }
                }
                else
                {
                    // If no ticket ID exists, start from P1
                    nextTrainID = "T1";
                }

                con.Close();
            }

            return nextTrainID;
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
        }
    }
}