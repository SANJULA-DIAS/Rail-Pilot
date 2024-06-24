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
    public partial class PassengerDataStationMaster : MetroFramework.Forms.MetroForm
    {
        public PassengerDataStationMaster()
        {
            InitializeComponent();
        }

        SqlDataAdapter da;
        SqlConnection con;

        private void PassengerDataStationMaster_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAPTOP-J36K2N86;Initial Catalog=TrainReserveSystem;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("Select * from Passenger", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
