using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace BusTracker
{
    public partial class DiningInfo : PhoneApplicationPage
    {
        public DiningInfo()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {

            string url = "https://secure.hosting.vt.edu/www.dining.vt.edu/hours/index.php?d=t";
            var uri = new Uri(url, UriKind.Absolute);

            string data = "d_month=" + 3 + "&d_day=" + 3 + "&d_year=" + 2013 + "&view=View+Date";

            var wc = new WebClient();

            wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted);

            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            wc.Headers["Content-Length"] = data.Length.ToString();
            wc.UploadStringAsync(uri, "POST", data);

        }

        private void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {

            try
            {
                MessageBox.Show(e.Result);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}