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
    public partial class ViewPJourney : MetroFramework.Forms.MetroForm
    {
        public ViewPJourney()
        {
            InitializeComponent();
        }

        private void ViewPJourney_Load(object sender, EventArgs e)
        {

        }
        private async Task initizated()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }
        public async void InitBrowser()
        {
            await initizated();

            string fromCity = fromcombobox.Text.ToString();
            string toCity = tocombobox.Text.ToString();

            // Generate the Google Maps URL with the source and destination cities
            string googleMapsUrl = $"https://www.google.com/maps/dir/{fromCity}+Railway+Station/{toCity}+Railway+Station/@7.9205519,79.8527376,8z/data=!4m2!4m1!3e3?entry=ttu";

            // Navigate the WebBrowser control to the Google Maps URL
            webView21.CoreWebView2.Navigate(googleMapsUrl);
        }

        private void tocombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitBrowser();
        }

        private void fromcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tocombobox.Items.Clear();
            tocombobox.ResetText();


            // Get the selected index of the first ComboBox
            int selectedIndex = fromcombobox.SelectedIndex;

            // Hide the corresponding item in the second ComboBox
            for (int i = 0; i < fromcombobox.Items.Count; i++)
            {
                if (i != selectedIndex)
                {
                    tocombobox.Items.Add(fromcombobox.Items[i]);
                }
            }
        }

        private void lbl_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
