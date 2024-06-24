
namespace CWGUIOFF
{
    partial class PassengerReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassengerReports));
            this.PassengerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetPassengers = new CWGUIOFF.DataSetPassengers();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.fromcombobox = new System.Windows.Forms.ComboBox();
            this.tocombobox = new System.Windows.Forms.ComboBox();
            this.PassengerTableAdapter = new CWGUIOFF.DataSetPassengersTableAdapters.PassengerTableAdapter();
            this.guna2PictureBox6 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox5 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            ((System.ComponentModel.ISupportInitialize)(this.PassengerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPassengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // PassengerBindingSource
            // 
            this.PassengerBindingSource.DataMember = "Passenger";
            this.PassengerBindingSource.DataSource = this.DataSetPassengers;
            // 
            // DataSetPassengers
            // 
            this.DataSetPassengers.DataSetName = "DataSetPassengers";
            this.DataSetPassengers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PassengerBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CWGUIOFF.PReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(2, 159);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(5);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1173, 772);
            this.reportViewer1.TabIndex = 0;
            // 
            // fromcombobox
            // 
            this.fromcombobox.FormattingEnabled = true;
            this.fromcombobox.Items.AddRange(new object[] {
            "Jaffna",
            "Kilinochchi",
            "Vavuniya",
            "Puttalam",
            "Chilaw",
            "Trincomalee",
            "Hingurakgoda",
            "Polonnaruwa",
            "Anuradhapura",
            "Kandy",
            "Nuwara Eliya",
            "Badulla",
            "Monaragala",
            "Ratnapura",
            "Negambo",
            "Colombo",
            "Bambalapitiya",
            "Dehiwala",
            "Moratuwa",
            "Panadura",
            "Kaluthara",
            "Hikkaduwa",
            "Galle",
            "Matara"});
            this.fromcombobox.Location = new System.Drawing.Point(230, 86);
            this.fromcombobox.Name = "fromcombobox";
            this.fromcombobox.Size = new System.Drawing.Size(232, 37);
            this.fromcombobox.TabIndex = 1;
            this.fromcombobox.SelectedIndexChanged += new System.EventHandler(this.fromcombobox_SelectedIndexChanged);
            // 
            // tocombobox
            // 
            this.tocombobox.FormattingEnabled = true;
            this.tocombobox.Items.AddRange(new object[] {
            "Jaffna",
            "Kilinochchi",
            "Vavuniya",
            "Puttalam",
            "Chilaw",
            "Trincomalee",
            "Hingurakgoda",
            "Polonnaruwa",
            "Anuradhapura",
            "Kandy",
            "Nuwara Eliya",
            "Badulla",
            "Monaragala",
            "Ratnapura",
            "Negambo",
            "Colombo",
            "Bambalapitiya",
            "Dehiwala",
            "Moratuwa",
            "Panadura",
            "Kaluthara",
            "Hikkaduwa",
            "Galle",
            "Matara"});
            this.tocombobox.Location = new System.Drawing.Point(676, 86);
            this.tocombobox.Name = "tocombobox";
            this.tocombobox.Size = new System.Drawing.Size(232, 37);
            this.tocombobox.TabIndex = 2;
            this.tocombobox.SelectedIndexChanged += new System.EventHandler(this.tocombobox_SelectedIndexChanged);
            // 
            // PassengerTableAdapter
            // 
            this.PassengerTableAdapter.ClearBeforeFill = true;
            // 
            // guna2PictureBox6
            // 
            this.guna2PictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox6.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox6.Image")));
            this.guna2PictureBox6.ImageRotate = 0F;
            this.guna2PictureBox6.Location = new System.Drawing.Point(460, 81);
            this.guna2PictureBox6.Name = "guna2PictureBox6";
            this.guna2PictureBox6.Size = new System.Drawing.Size(230, 47);
            this.guna2PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox6.TabIndex = 101;
            this.guna2PictureBox6.TabStop = false;
            this.guna2PictureBox6.UseTransparentBackground = true;
            // 
            // guna2PictureBox5
            // 
            this.guna2PictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox5.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox5.Image")));
            this.guna2PictureBox5.ImageRotate = 0F;
            this.guna2PictureBox5.Location = new System.Drawing.Point(10, 82);
            this.guna2PictureBox5.Name = "guna2PictureBox5";
            this.guna2PictureBox5.Size = new System.Drawing.Size(230, 47);
            this.guna2PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox5.TabIndex = 100;
            this.guna2PictureBox5.TabStop = false;
            this.guna2PictureBox5.UseTransparentBackground = true;
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox2.Image")));
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(367, -20);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(485, 100);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox2.TabIndex = 102;
            this.guna2PictureBox2.TabStop = false;
            this.guna2PictureBox2.UseTransparentBackground = true;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.DarkBlue;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(1088, 0);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox2.TabIndex = 104;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.DarkBlue;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1133, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 103;
            // 
            // PassengerReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1177, 816);
            this.Controls.Add(this.guna2ControlBox2);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.fromcombobox);
            this.Controls.Add(this.tocombobox);
            this.Controls.Add(this.guna2PictureBox2);
            this.Controls.Add(this.guna2PictureBox6);
            this.Controls.Add(this.guna2PictureBox5);
            this.Controls.Add(this.reportViewer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PassengerReports";
            this.Padding = new System.Windows.Forms.Padding(35, 109, 35, 36);
            this.Text = "PassengerReports";
            this.Load += new System.EventHandler(this.PassengerReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PassengerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPassengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PassengerBindingSource;
        private DataSetPassengers DataSetPassengers;
        private DataSetPassengersTableAdapters.PassengerTableAdapter PassengerTableAdapter;
        private System.Windows.Forms.ComboBox fromcombobox;
        private System.Windows.Forms.ComboBox tocombobox;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox6;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox5;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}