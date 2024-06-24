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
using System.Net.Mail;
using System.Net;

namespace CWGUIOFF
{
    public partial class DriverUpdate : MetroFramework.Forms.MetroForm
    {
        public DriverUpdate()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;

        private void DriverUpdate_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
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

                con.Open();
                cmd = new SqlCommand("UPDATE Driver SET Driver_Name = @Driver_Name, Age = @Age, Driver_Exp = @Driver_Exp, Relationship = @Relationship, email = @email, Driver_TP = @Driver_TP WHERE Driver_Id = @Driver_Id", con);
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
                    MessageBox.Show("Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Send email notification
                    string subject = "Driver Profile Updated";
                    string body = $"Dear {txt_name.Text},\n\nYour driver profile has been successfully updated.\n\nBest regards,\nThe TrainReserveSystem Team";
                    string fromEmail = "RAILPILOT@outlook.com"; // Replace with the sender email address
                    string toEmail = txt_email.Text;

                    using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("RAILPILOT@outlook.com", "TRAILRESERVER#");

                        using (MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body))
                        {
                            smtpClient.Send(mailMessage);
                        }
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
