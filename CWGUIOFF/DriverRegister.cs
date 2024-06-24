using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net.Mail;

namespace CWGUIOFF
{
    public partial class DriverRegister : MetroFramework.Forms.MetroForm

    {
        public DriverRegister()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;

        private void DriverRegister_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
            txt_driverid.ReadOnly = true;
            txt_driverid.Text = GetNextDriverID();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_driverid.Text) || string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(comboBox1.Text)
                    || string.IsNullOrEmpty(txt_email.Text) || string.IsNullOrEmpty(txt_telephone.Text) || string.IsNullOrEmpty(txt_exp.Text))
                {
                    throw new ArgumentException("Please fill in all the required fields.");
                }

                if (Regex.IsMatch(txt_name.Text, @"\d")) //digit character  
                {
                    throw new ArgumentException("Name cannot contain numbers.");
                }

                if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    throw new ArgumentException("Invalid email address.");
                }

                if (!Regex.IsMatch(txt_telephone.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
                {
                    throw new ArgumentException("Invalid telephone number.");
                }
                if (!int.TryParse(txt_age.Text, out int age))
                {
                    throw new ArgumentException("Invalid age. Age must be a numeric value.");
                }

                // Validate Experience
                if (!int.TryParse(txt_exp.Text, out int exp))
                {
                    throw new ArgumentException("Invalid Experience. Experience must be a numeric value.");
                }

                if (age < 21 || age > 55)
                {
                    throw new ArgumentException("To be a Driver your age must between 21 to 55");
                }


                con.Open();
                cmd = new SqlCommand("INSERT INTO Driver VALUES (@Driver_Id, @Driver_Name, @Age, @Driver_Exp, @Relationship, @email, @Driver_TP)", con);
                cmd.Parameters.AddWithValue("@Driver_Id", txt_driverid.Text);
                cmd.Parameters.AddWithValue("@Driver_Name", txt_name.Text);
                cmd.Parameters.AddWithValue("@Age", txt_age.Text);
                cmd.Parameters.AddWithValue("@Driver_Exp", txt_exp.Text);
                cmd.Parameters.AddWithValue("@Relationship", comboBox1.Text);
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.Parameters.AddWithValue("@Driver_TP", txt_telephone.Text);

                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {

                    string subject = "RAIL PILOT: Driver Registration Confirmation";
                    string body = $"Dear {txt_name.Text},\n\nThank you for registering as a driver with RAIL PILOT.\n\nYour Driver ID: {txt_driverid.Text}\n\nWe appreciate your interest in our service. Your driver registration is now complete!\n\nIf you have any queries or require further information, feel free to contact us.\n\nBest regards,\nThe RAIL PILOT Team";

                    string fromEmail = "RAILPILOT@outlook.com";
                    string driverEmail = txt_email.Text;

                    using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential("RAILPILOT@outlook.com", "TRAILRESERVER#");

                        using (MailMessage mailMessage = new MailMessage(fromEmail, driverEmail, subject, body))
                        {
                            smtpClient.Send(mailMessage);
                        }
                    }

                    MessageBox.Show("Registered Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    int previousDriverID;
                    bool success = int.TryParse(txt_driverid.Text.Substring(1), out previousDriverID);

                    // If the previous train ID could be parsed successfully, increment it
                    if (success)
                    {
                        int nextTrainID = previousDriverID + 1;
                        txt_driverid.Text = "D" + nextTrainID;
                        txt_name.Clear();
                        txt_age.Clear();
                        txt_exp.Clear();
                        txt_email.Clear();
                        txt_telephone.Clear();
                        comboBox1.ResetText();
                    }
                }
                else
                {
                    MessageBox.Show("Something's wrong, please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetNextDriverID()
        {
            string nextDriverID = string.Empty;

            using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True"))
            {
                con.Open();

                // Retrieve the maximum ticket ID from the Passenger table
                SqlCommand cmd = new SqlCommand("SELECT MAX(Driver_ID) FROM Driver", con);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string currentDriverID = result.ToString();
                    string numericPart = currentDriverID.Substring(1); // Extract the numeric part of the ID
                    int numericValue;

                    if (int.TryParse(numericPart, out numericValue))
                    {
                        // If a valid numeric part exists, increment it by 1 and concatenate with the prefix
                        nextDriverID = "D" + (numericValue + 1);
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
                    nextDriverID = "D1";
                }

                con.Close();
            }

            return nextDriverID;
        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            txt_name.Clear();
            txt_age.Clear();
            txt_exp.Clear();
            txt_email.Clear();
            txt_telephone.Clear();
            comboBox1.ResetText();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
