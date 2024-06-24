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
    public partial class Home : Krypton.Toolkit.KryptonForm
    {
        public Home()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)  // TO MOVE THE WINDOW
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
            BookTrain obj = new BookTrain();
            obj.ShowDialog();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            UpdateTicket obj = new UpdateTicket();
            obj.ShowDialog();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            ShowConfirmationMessageBox();
        }
        public void ShowConfirmationMessageBox()  // UPDATE DELETE MESSAGE BOX
        {
            CustomMessageBox customMessageBox = new CustomMessageBox("Are you sure to delete your ticket?\nYou can still update your ticket.");

            DialogResult result = customMessageBox.ShowDialog();

            if (result == DialogResult.Yes)
            {
                // Open the update form
                UpdateTicket updateForm = new UpdateTicket();
                updateForm.ShowDialog();
            }
            else if (result == DialogResult.No)
            {
                // Open the delete form
                DeleteTicket deleteForm = new DeleteTicket();
                deleteForm.ShowDialog();
            }
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            ViewPJourney obj = new ViewPJourney();
            obj.ShowDialog();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }

    public class CustomMessageBox : Form    // custome created message box
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent(message);
        }

        private void InitializeComponent(string message)
        {
            this.Text = "Confirmation";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(300, 120);

            Label lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.AutoSize = true;
            lblMessage.Location = new System.Drawing.Point(20, 20);

            Button btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.DialogResult = DialogResult.Yes;
            btnUpdate.Location = new System.Drawing.Point(20, 60);
            btnUpdate.Click += (sender, e) => this.Close();

            Button btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.DialogResult = DialogResult.No;
            btnDelete.Location = new System.Drawing.Point(120, 60);
            btnDelete.Click += (sender, e) => this.Close();

            this.Controls.Add(lblMessage);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
        }
    }
}
