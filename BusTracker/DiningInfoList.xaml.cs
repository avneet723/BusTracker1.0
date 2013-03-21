using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text.RegularExpressions;

namespace BusTracker
{
    public partial class DiningInfoList : PhoneApplicationPage
    {

        public DiningInfoList()
        {
            InitializeComponent();
            this.dateTime = DateTime.Now;
        }

        public DateTime dateTime { get; set; }

        private void download()
        {

            // The URL for the Dining Hours Website
            string url = "https://secure.hosting.vt.edu/www.dining.vt.edu/hours/index.php?d=t";
            var uri = new Uri(url, UriKind.Absolute);

            // Pick the Month, Day, and Year from the datepicker
            string data = "d_month=" + dateTime.Month + "&d_day=" + dateTime.Day + "&d_year=" + dateTime.Year + "&view=View+Date";

            var wc = new WebClient();

            wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted); // When our HTTP Post is Finished, Go to the wc_UploadStringCompleted Event Handler

            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded"; // Specify the Content-Type ...
            wc.Headers["Content-Length"] = data.Length.ToString(); // ... and the Content-Length
            wc.UploadStringAsync(uri, "POST", data); // Begin the HTTP Post

        }

        private void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            HD.LoadHtml(e.Result);




            try
            {

                var abp = HD.DocumentNode.SelectSingleNode("//body//li//text()").InnerText.Trim();
                var time = HD.DocumentNode.SelectSingleNode("//body//li//ul").InnerText.Trim();
                System.Diagnostics.Debug.WriteLine(abp + " | " + time);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                foreach (var node in HD.DocumentNode.SelectNodes("//body//li"))
                {
                    // System.Diagnostics.Debug.WriteLine("Example :: {0}", node.InnerText);
                    string[] lines = Regex.Split(node.InnerText, "\r\n");
                    System.Diagnostics.Debug.WriteLine(lines[0]);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void date_change(object sender, Microsoft.Phone.Controls.DateTimeValueChangedEventArgs e)
        {
        	download();
        }

        private void dining_hall_change(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	
        }
    }
}