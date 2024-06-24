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
using System.Net.Mail;

namespace CWGUIOFF
{
    public partial class DeleteTicket : MetroFramework.Forms.MetroForm
    {
        public DeleteTicket()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;

        private void DeleteTicket_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
        }

        private void BTN_DELETE_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                // Retrieve passenger name and email from the database
                cmd = new SqlCommand("SELECT Passenger_Name, Email FROM Passenger WHERE PassengerTicket_Id = @ticketNumber", con);
                cmd.Parameters.AddWithValue("@ticketNumber", txt_tiketnumber.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Check if data is available
                {
                    string passengerName = reader["Passenger_Name"].ToString();
                    string passengerEmail = reader["Email"].ToString();
                    reader.Close(); // Close the reader after retrieving the data

                    cmd = new SqlCommand("DELETE FROM Passenger WHERE PassengerTicket_Id = @ticketNumber", con);
                    cmd.Parameters.AddWithValue("@ticketNumber", txt_tiketnumber.Text);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        string subject = "RAIL PILOT: Train Ticket Cancellation";
                        string body = $"Hello {passengerName},\n\nWe regret to inform you that your train ticket with the following details has been canceled:\n\nTicket Number: {txt_tiketnumber.Text}\n\nIf you did not initiate this cancellation or have any questions, please contact our customer support immediately.\n\nWe apologize for any inconvenience this may cause.\n\nBest regards,\nThe RAIL PILOT Team";

                        string fromEmail = "RAILPILOT@outlook.com";
                        string toEmail = passengerEmail; // Use the retrieved passenger email

                        using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                        {
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential("RAILPILOT@outlook.com", "TRAILRESERVER#");

                            using (MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body))
                            {
                                smtpClient.Send(mailMessage);
                            }
                        }
                        MessageBox.Show("Reservation Deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data cannot be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Invalid ticket number or passenger data not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
