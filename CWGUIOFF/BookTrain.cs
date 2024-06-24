using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;

namespace CWGUIOFF
{
    public partial class BookTrain : Form
    {
        public BookTrain()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += to_combobox_SelectedIndexChanged;

        }

        SqlConnection con;
        SqlCommand cmd;


        private void BookTrain_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
            txt_cost.ReadOnly = true;
            txt_tiketnumber.ReadOnly = true;
            txt_tiketnumber.Text = GetNextTicketID();

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

        private void btn_booktrain_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_tiketnumber.Text) || string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(to_combobox.Text) ||
                    string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(txt_email.Text) || string.IsNullOrEmpty(txt_telephone.Text))
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

                con.Open();
                cmd = new SqlCommand("INSERT INTO Passenger VALUES (@ticketNumber, @name, @to, @from, @cost, @email, @telephone)", con);
                cmd.Parameters.AddWithValue("@ticketNumber", txt_tiketnumber.Text);
                cmd.Parameters.AddWithValue("@name", txt_name.Text);
                cmd.Parameters.AddWithValue("@to", to_combobox.Text);
                cmd.Parameters.AddWithValue("@from", comboBox1.Text);
                cmd.Parameters.AddWithValue("@cost", txt_cost.Text);
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.Parameters.AddWithValue("@telephone", txt_telephone.Text);

                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    string subject = "Train Booking Confirmation";
                    string body = $"Dear {txt_name.Text},\n\nThank you for booking a train. Your ticket number is: {txt_tiketnumber.Text}\n\nPlease make the payment at the designated counter in the departure station before your journey.\n\nEnjoy your journey!.\n\nBest regards,\nThe RAIL PILOT Team";
                    string fromEmail = "RAILPILOT@outlook.com";
                    string toEmail = txt_email.Text;

                    using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))  // EMAIL CODE
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential("RAILPILOT@outlook.com", "TRAILRESERVER#");

                        using ( MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body))
                        {
                            smtpClient.Send(mailMessage);
                        }
                    }

                    MessageBox.Show("Reserved Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    to_combobox.ResetText();
                    comboBox1.ResetText();
                    txt_name.Clear();
                    txt_cost.Clear();
                    txt_email.Clear();
                    txt_telephone.Clear();

                    int previousTicketID;
                    bool success = int.TryParse(txt_tiketnumber.Text.Substring(1), out previousTicketID);

                    // If the previous train ID could be parsed successfully, increment it
                    if (success)
                    {
                        int nextTrainID = previousTicketID + 1;
                        txt_tiketnumber.Text = "P" + nextTrainID;
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

        public void to_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal cost = 0;

                if (comboBox1.SelectedIndex == 0)
                {
                    if (to_combobox.SelectedIndex == 1) cost = 750; // Jaffna to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 900; // Jaffna to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1050; // Jaffna to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1200; // Jaffna to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1350; // Jaffna to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1500; // Jaffna to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1650; // Jaffna to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1800; // Jaffna to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1950; // Jaffna to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 2100; // Jaffna to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 2250; // Jaffna to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 2400; // Jaffna to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 2550; // Jaffna to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 2700; // Jaffna to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 2850; // Jaffna to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 3000; // Jaffna to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 3150; // Jaffna to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 3300; // Jaffna to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 3450; // Jaffna to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 3600; // Jaffna to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 3750; // Jaffna to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 3900; // Jaffna to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 4050; // Jaffna to Matara
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 750; // Kilinochchi to Jaffna
                    else if (to_combobox.SelectedIndex == 2) cost = 900; // Kilinochchi to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1050; // Kilinochchi to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1200; // Kilinochchi to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1350; // Kilinochchi to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1500; // Kilinochchi to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1650; // Kilinochchi to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1800; // Kilinochchi to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1950; // Kilinochchi to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 2100; // Kilinochchi to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 2250; // Kilinochchi to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 2400; // Kilinochchi to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 2550; // Kilinochchi to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 2700; // Kilinochchi to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 2850; // Kilinochchi to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 3000; // Kilinochchi to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 3150; // Kilinochchi to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 3300; // Kilinochchi to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 3450; // Kilinochchi to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 3600; // Kilinochchi to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 3750; // Kilinochchi to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 3900; // Kilinochchi to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 4050; // Kilinochchi to Matara
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 900; // Vavuniya to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 750; // Vavuniya to Kilinochchi
                    else if (to_combobox.SelectedIndex == 3) cost = 900; // Vavuniya to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1050; // Vavuniya to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1200; // Vavuniya to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1350; // Vavuniya to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1500; // Vavuniya to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1650; // Vavuniya to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1800; // Vavuniya to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1950; // Vavuniya to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 2100; // Vavuniya to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 2250; // Vavuniya to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 2400; // Vavuniya to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 2550; // Vavuniya to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 2700; // Vavuniya to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 2850; // Vavuniya to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 3000; // Vavuniya to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 3150; // Vavuniya to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 3300; // Vavuniya to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 3450; // Vavuniya to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 3600; // Vavuniya to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 3750; // Vavuniya to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 3900; // Vavuniya to Matara
                }

                else if (comboBox1.SelectedIndex == 3)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 1050; // Puttalam to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 1200; // Puttalam to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 900; // Puttalam to Vavuniya
                    else if (to_combobox.SelectedIndex == 4) cost = 750; // Puttalam to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 900; // Puttalam to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1050; // Puttalam to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1200; // Puttalam to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1350; // Puttalam to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1500; // Puttalam to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1650; // Puttalam to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1800; // Puttalam to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1950; // Puttalam to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 2100; // Puttalam to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 2250; // Puttalam to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 2400; // Puttalam to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 2550; // Puttalam to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 2700; // Puttalam to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 2850; // Puttalam to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 3000; // Puttalam to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 3150; // Puttalam to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 3300; // Puttalam to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 3450; // Puttalam to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 3600; // Puttalam to Matara
                }

                else if (comboBox1.SelectedIndex == 4)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 1200; // Chilaw to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 1350; // Chilaw to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 1050; // Chilaw to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 750; // Chilaw to Puttalam
                    else if (to_combobox.SelectedIndex == 5) cost = 900; // Chilaw to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1050; // Chilaw to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1200; // Chilaw to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1350; // Chilaw to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1500; // Chilaw to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1650; // Chilaw to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1800; // Chilaw to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1950; // Chilaw to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 2100; // Chilaw to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 2250; // Chilaw to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 2400; // Chilaw to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 2550; // Chilaw to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 2700; // Chilaw to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 2850; // Chilaw to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 3000; // Chilaw to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 3150; // Chilaw to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 3300; // Chilaw to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 3450; // Chilaw to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 3600; // Chilaw to Matara
                }
                else if (comboBox1.SelectedIndex == 5)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 1350; // Trincomalee to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 1500; // Trincomalee to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 1200; // Trincomalee to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 900; // Trincomalee to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 900; // Trincomalee to Chilaw
                    else if (to_combobox.SelectedIndex == 6) cost = 750; // Trincomalee to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 900; // Trincomalee to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1050; // Trincomalee to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1200; // Trincomalee to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1350; // Trincomalee to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1500; // Trincomalee to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1650; // Trincomalee to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1800; // Trincomalee to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1950; // Trincomalee to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 2100; // Trincomalee to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 2250; // Trincomalee to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 2400; // Trincomalee to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 2550; // Trincomalee to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 2700; // Trincomalee to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 2850; // Trincomalee to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 3000; // Trincomalee to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 3150; // Trincomalee to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 3300; // Trincomalee to Matara
                }

                else if (comboBox1.SelectedIndex == 6)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 1500; // Hingurakgoda to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 1650; // Hingurakgoda to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 1350; // Hingurakgoda to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1050; // Hingurakgoda to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1050; // Hingurakgoda to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 750; // Hingurakgoda to Trincomalee
                    else if (to_combobox.SelectedIndex == 7) cost = 750; // Hingurakgoda to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 900; // Hingurakgoda to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1050; // Hingurakgoda to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1200; // Hingurakgoda to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1350; // Hingurakgoda to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1500; // Hingurakgoda to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1650; // Hingurakgoda to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1800; // Hingurakgoda to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1950; // Hingurakgoda to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 2100; // Hingurakgoda to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 2250; // Hingurakgoda to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 2400; // Hingurakgoda to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 2550; // Hingurakgoda to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 2700; // Hingurakgoda to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 2850; // Hingurakgoda to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 3000; // Hingurakgoda to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 3150; // Hingurakgoda to Matara
                }

                else if (comboBox1.SelectedIndex == 7)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 1650; // Polonnaruwa to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 1800; // Polonnaruwa to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 1500; // Polonnaruwa to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1200; // Polonnaruwa to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1200; // Polonnaruwa to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 900; // Polonnaruwa to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 750; // Polonnaruwa to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 8) cost = 750; // Polonnaruwa to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 900; // Polonnaruwa to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1050; // Polonnaruwa to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1200; // Polonnaruwa to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1350; // Polonnaruwa to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1500; // Polonnaruwa to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1650; // Polonnaruwa to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1800; // Polonnaruwa to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1950; // Polonnaruwa to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 2100; // Polonnaruwa to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 2250; // Polonnaruwa to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 2400; // Polonnaruwa to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 2550; // Polonnaruwa to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 2700; // Polonnaruwa to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 2850; // Polonnaruwa to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 3000; // Polonnaruwa to Matara
                }
                else if (comboBox1.SelectedIndex == 8)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 1800; // Anuradhapura to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 1950; // Anuradhapura to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 1650; // Anuradhapura to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1350; // Anuradhapura to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1350; // Anuradhapura to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1050; // Anuradhapura to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 900; // Anuradhapura to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 750; // Anuradhapura to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 9) cost = 750; // Anuradhapura to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 900; // Anuradhapura to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1050; // Anuradhapura to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1200; // Anuradhapura to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1350; // Anuradhapura to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1500; // Anuradhapura to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1650; // Anuradhapura to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1800; // Anuradhapura to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1950; // Anuradhapura to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 2100; // Anuradhapura to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 2250; // Anuradhapura to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 2400; // Anuradhapura to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 2550; // Anuradhapura to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 2700; // Anuradhapura to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 2850; // Anuradhapura to Matara
                }

                else if (comboBox1.SelectedIndex == 9)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 1950; // Kandy to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 2100; // Kandy to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 1800; // Kandy to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1500; // Kandy to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1500; // Kandy to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1200; // Kandy to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1050; // Kandy to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 900; // Kandy to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 750; // Kandy to Anuradhapura
                    else if (to_combobox.SelectedIndex == 10) cost = 750; // Kandy to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 900; // Kandy to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1050; // Kandy to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1200; // Kandy to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1350; // Kandy to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1500; // Kandy to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1650; // Kandy to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1800; // Kandy to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1950; // Kandy to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 2100; // Kandy to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 2250; // Kandy to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 2400; // Kandy to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 2550; // Kandy to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 2700; // Kandy to Matara
                }
                else if (comboBox1.SelectedIndex == 10)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 2100; // Nuwara Eliya to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 2250; // Nuwara Eliya to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 1950; // Nuwara Eliya to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1650; // Nuwara Eliya to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1650; // Nuwara Eliya to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1350; // Nuwara Eliya to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1200; // Nuwara Eliya to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1050; // Nuwara Eliya to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 900; // Nuwara Eliya to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 750; // Nuwara Eliya to Kandy
                    else if (to_combobox.SelectedIndex == 11) cost = 750; // Nuwara Eliya to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 900; // Nuwara Eliya to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1050; // Nuwara Eliya to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1200; // Nuwara Eliya to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1350; // Nuwara Eliya to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1500; // Nuwara Eliya to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1650; // Nuwara Eliya to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1800; // Nuwara Eliya to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1950; // Nuwara Eliya to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 2100; // Nuwara Eliya to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 2250; // Nuwara Eliya to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 2400; // Nuwara Eliya to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 2550; // Nuwara Eliya to Matara
                }
                else if (comboBox1.SelectedIndex == 11)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 2250; // Badulla to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 2400; // Badulla to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 2100; // Badulla to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1800; // Badulla to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1800; // Badulla to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1500; // Badulla to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1350; // Badulla to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1200; // Badulla to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1050; // Badulla to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 900; // Badulla to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 750; // Badulla to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 12) cost = 750; // Badulla to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 900; // Badulla to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1050; // Badulla to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1200; // Badulla to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1350; // Badulla to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1500; // Badulla to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1650; // Badulla to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1800; // Badulla to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1950; // Badulla to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 2100; // Badulla to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 2250; // Badulla to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 2400; // Badulla to Matara
                }
                else if (comboBox1.SelectedIndex == 12)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 2400; // Monaragala to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 2550; // Monaragala to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 2250; // Monaragala to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 1950; // Monaragala to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 1950; // Monaragala to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1650; // Monaragala to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1500; // Monaragala to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1350; // Monaragala to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1200; // Monaragala to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1050; // Monaragala to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 900; // Monaragala to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 750; // Monaragala to Badulla
                    else if (to_combobox.SelectedIndex == 13) cost = 750; // Monaragala to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 900; // Monaragala to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1050; // Monaragala to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1200; // Monaragala to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1350; // Monaragala to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1500; // Monaragala to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1650; // Monaragala to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1800; // Monaragala to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 1950; // Monaragala to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 2100; // Monaragala to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 2250; // Monaragala to Matara
                }
                else if (comboBox1.SelectedIndex == 13)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 2550; // Ratnapura to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 2700; // Ratnapura to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 2400; // Ratnapura to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 2100; // Ratnapura to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 2100; // Ratnapura to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 1800; // Ratnapura to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1650; // Ratnapura to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1500; // Ratnapura to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1350; // Ratnapura to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1200; // Ratnapura to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1050; // Ratnapura to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 900; // Ratnapura to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 750; // Ratnapura to Monaragala
                    else if (to_combobox.SelectedIndex == 14) cost = 750; // Ratnapura to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 900; // Ratnapura to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1050; // Ratnapura to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1200; // Ratnapura to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1350; // Ratnapura to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1500; // Ratnapura to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1650; // Ratnapura to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 1800; // Ratnapura to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 1950; // Ratnapura to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 2100; // Ratnapura to Matara
                }
                else if (comboBox1.SelectedIndex == 14)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 2850; // Negambo to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 3000; // Negambo to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 2700; // Negambo to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 2400; // Negambo to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 2400; // Negambo to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 2100; // Negambo to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 1950; // Negambo to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1800; // Negambo to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1650; // Negambo to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1500; // Negambo to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1350; // Negambo to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1200; // Negambo to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1050; // Negambo to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 900; // Negambo to Ratnapura
                    else if (to_combobox.SelectedIndex == 15) cost = 750; // Negambo to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 900; // Negambo to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1050; // Negambo to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1200; // Negambo to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1350; // Negambo to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1500; // Negambo to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 1650; // Negambo to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 1800; // Negambo to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 1950; // Negambo to Matara
                }
                else if (comboBox1.SelectedIndex == 15)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 3000; // Colombo to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 3150; // Colombo to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 2850; // Colombo to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 2550; // Colombo to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 2550; // Colombo to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 2250; // Colombo to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 2100; // Colombo to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 1950; // Colombo to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1800; // Colombo to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1650; // Colombo to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1500; // Colombo to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1350; // Colombo to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1200; // Colombo to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 900; // Colombo to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 750; // Colombo to Negambo
                    else if (to_combobox.SelectedIndex == 16) cost = 750; // Colombo to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 900; // Colombo to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1050; // Colombo to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1200; // Colombo to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1350; // Colombo to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 1500; // Colombo to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 1650; // Colombo to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 1800; // Colombo to Matara
                }
                else if (comboBox1.SelectedIndex == 16)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 3150; // Bambalapitiya to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 3300; // Bambalapitiya to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 3000; // Bambalapitiya to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 2700; // Bambalapitiya to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 2700; // Bambalapitiya to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 2400; // Bambalapitiya to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 2250; // Bambalapitiya to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 2100; // Bambalapitiya to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 1950; // Bambalapitiya to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1800; // Bambalapitiya to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1650; // Bambalapitiya to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1500; // Bambalapitiya to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1350; // Bambalapitiya to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1200; // Bambalapitiya to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 900; // Bambalapitiya to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 750; // Bambalapitiya to Colombo
                    else if (to_combobox.SelectedIndex == 17) cost = 750; // Bambalapitiya to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 900; // Bambalapitiya to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1050; // Bambalapitiya to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1200; // Bambalapitiya to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 1350; // Bambalapitiya to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 1500; // Bambalapitiya to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 1650; // Bambalapitiya to Matara
                }
                else if (comboBox1.SelectedIndex == 17)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 3300; // Dehiwala to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 3450; // Dehiwala to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 3150; // Dehiwala to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 2850; // Dehiwala to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 2850; // Dehiwala to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 2550; // Dehiwala to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 2400; // Dehiwala to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 2250; // Dehiwala to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 2100; // Dehiwala to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 1950; // Dehiwala to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1800; // Dehiwala to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1650; // Dehiwala to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1500; // Dehiwala to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1350; // Dehiwala to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1050; // Dehiwala to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 900; // Dehiwala to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 750; // Dehiwala to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 18) cost = 750; // Dehiwala to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 900; // Dehiwala to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1050; // Dehiwala to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 1200; // Dehiwala to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 1350; // Dehiwala to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 1500; // Dehiwala to Matara
                }
                else if (comboBox1.SelectedIndex == 18)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 3450; // Moratuwa to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 3600; // Moratuwa to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 3300; // Moratuwa to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 3000; // Moratuwa to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 3000; // Moratuwa to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 2700; // Moratuwa to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 2550; // Moratuwa to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 2400; // Moratuwa to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 2250; // Moratuwa to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 2100; // Moratuwa to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 1950; // Moratuwa to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1800; // Moratuwa to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1650; // Moratuwa to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1500; // Moratuwa to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1200; // Moratuwa to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1050; // Moratuwa to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 900; // Moratuwa to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 750; // Moratuwa to Dehiwala
                    else if (to_combobox.SelectedIndex == 19) cost = 750; // Moratuwa to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 900; // Moratuwa to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 1050; // Moratuwa to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 1200; // Moratuwa to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 1350; // Moratuwa to Matara
                }
                else if (comboBox1.SelectedIndex == 19)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 3600; // Panadura to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 3750; // Panadura to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 3450; // Panadura to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 3150; // Panadura to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 3150; // Panadura to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 2850; // Panadura to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 2700; // Panadura to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 2550; // Panadura to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 2400; // Panadura to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 2250; // Panadura to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 2100; // Panadura to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 1950; // Panadura to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1800; // Panadura to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1650; // Panadura to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1350; // Panadura to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1200; // Panadura to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1050; // Panadura to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 900; // Panadura to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 750; // Panadura to Moratuwa
                    else if (to_combobox.SelectedIndex == 20) cost = 750; // Panadura to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 900; // Panadura to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 1050; // Panadura to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 1200; // Panadura to Matara
                }
                else if (comboBox1.SelectedIndex == 20)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 3750; // Kaluthara to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 3900; // Kaluthara to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 3600; // Kaluthara to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 3300; // Kaluthara to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 3300; // Kaluthara to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 3000; // Kaluthara to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 2850; // Kaluthara to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 2700; // Kaluthara to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 2550; // Kaluthara to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 2400; // Kaluthara to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 2250; // Kaluthara to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 2100; // Kaluthara to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 1950; // Kaluthara to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1800; // Kaluthara to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1500; // Kaluthara to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1350; // Kaluthara to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1200; // Kaluthara to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1050; // Kaluthara to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 900; // Kaluthara to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 750; // Kaluthara to Panadura
                    else if (to_combobox.SelectedIndex == 21) cost = 750; // Kaluthara to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 900; // Kaluthara to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 1050; // Kaluthara to Matara
                }
                else if (comboBox1.SelectedIndex == 21)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 3900; // Hikkaduwa to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 4050; // Hikkaduwa to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 3750; // Hikkaduwa to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 3450; // Hikkaduwa to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 3450; // Hikkaduwa to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 3150; // Hikkaduwa to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 3000; // Hikkaduwa to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 2850; // Hikkaduwa to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 2700; // Hikkaduwa to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 2550; // Hikkaduwa to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 2400; // Hikkaduwa to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 2250; // Hikkaduwa to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 2100; // Hikkaduwa to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 1950; // Hikkaduwa to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1650; // Hikkaduwa to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1500; // Hikkaduwa to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1350; // Hikkaduwa to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1200; // Hikkaduwa to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1050; // Hikkaduwa to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 900; // Hikkaduwa to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 750; // Hikkaduwa to Kaluthara
                    else if (to_combobox.SelectedIndex == 22) cost = 750; // Hikkaduwa to Galle
                    else if (to_combobox.SelectedIndex == 23) cost = 900; // Hikkaduwa to Matara
                }
                else if (comboBox1.SelectedIndex == 22)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 4050; // Galle to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 4200; // Galle to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 3900; // Galle to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 3600; // Galle to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 3600; // Galle to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 3300; // Galle to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 3150; // Galle to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 3000; // Galle to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 2850; // Galle to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 2700; // Galle to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 2550; // Galle to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 2400; // Galle to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 2250; // Galle to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 2100; // Galle to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1800; // Galle to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1650; // Galle to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1500; // Galle to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1350; // Galle to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1200; // Galle to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1050; // Galle to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 900; // Galle to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 750; // Galle to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 23) cost = 750; // Galle to Matara
                }
                else if (comboBox1.SelectedIndex == 23)
                {
                    if (to_combobox.SelectedIndex == 0) cost = 4200; // Matara to Jaffna
                    else if (to_combobox.SelectedIndex == 1) cost = 4350; // Matara to Kilinochchi
                    else if (to_combobox.SelectedIndex == 2) cost = 4050; // Matara to Vavuniya
                    else if (to_combobox.SelectedIndex == 3) cost = 3750; // Matara to Puttalam
                    else if (to_combobox.SelectedIndex == 4) cost = 3750; // Matara to Chilaw
                    else if (to_combobox.SelectedIndex == 5) cost = 3450; // Matara to Trincomalee
                    else if (to_combobox.SelectedIndex == 6) cost = 3300; // Matara to Hingurakgoda
                    else if (to_combobox.SelectedIndex == 7) cost = 3150; // Matara to Polonnaruwa
                    else if (to_combobox.SelectedIndex == 8) cost = 3000; // Matara to Anuradhapura
                    else if (to_combobox.SelectedIndex == 9) cost = 2850; // Matara to Kandy
                    else if (to_combobox.SelectedIndex == 10) cost = 2700; // Matara to Nuwara Eliya
                    else if (to_combobox.SelectedIndex == 11) cost = 2550; // Matara to Badulla
                    else if (to_combobox.SelectedIndex == 12) cost = 2400; // Matara to Monaragala
                    else if (to_combobox.SelectedIndex == 13) cost = 2250; // Matara to Ratnapura
                    else if (to_combobox.SelectedIndex == 14) cost = 1950; // Matara to Negambo
                    else if (to_combobox.SelectedIndex == 15) cost = 1800; // Matara to Colombo
                    else if (to_combobox.SelectedIndex == 16) cost = 1650; // Matara to Bambalapitiya
                    else if (to_combobox.SelectedIndex == 17) cost = 1500; // Matara to Dehiwala
                    else if (to_combobox.SelectedIndex == 18) cost = 1350; // Matara to Moratuwa
                    else if (to_combobox.SelectedIndex == 19) cost = 1200; // Matara to Panadura
                    else if (to_combobox.SelectedIndex == 20) cost = 1050; // Matara to Kaluthara
                    else if (to_combobox.SelectedIndex == 21) cost = 900; // Matara to Hikkaduwa
                    else if (to_combobox.SelectedIndex == 22) cost = 750; // Matara to Galle
                }



                txt_cost.Text = cost.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void from_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            to_combobox.Items.Clear();
            to_combobox.ResetText();

            int selectedIndex = comboBox1.SelectedIndex; // TO CHECK COMBO BOXES SAME VALUE

            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (i != selectedIndex)
                {
                    to_combobox.Items.Add(comboBox1.Items[i]);
                }
            }
        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            to_combobox.ResetText();
            comboBox1.ResetText();
            txt_name.Clear();
            txt_cost.Clear();
            txt_email.Clear();
            txt_telephone.Clear();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string GetNextTicketID()   // GET NEXT TICKET ID FROM DATABASE
        {
            string nextTicketID = string.Empty;

            using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True"))
            {
                con.Open();

                // Retrieve the maximum ticket ID from the Passenger table
                SqlCommand cmd = new SqlCommand("SELECT MAX(PassengerTicket_Id) FROM Passenger", con);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string currentTicketID = result.ToString();
                    string numericPart = currentTicketID.Substring(1); // Extract the numeric part of the ID
                    int numericValue;

                    if (int.TryParse(numericPart, out numericValue))
                    {
                        // If a valid numeric part exists, increment it by 1 and concatenate with the prefix
                        nextTicketID = "P" + (numericValue + 1);
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
                    nextTicketID = "P1";
                }

                con.Close();
            }

            return nextTicketID;
        }

    }
}
