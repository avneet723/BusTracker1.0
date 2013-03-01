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
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            // Current date and time
            DateTime now = DateTime.Now;
            string url = "https://secure.hosting.vt.edu/www.dining.vt.edu/hours/index.php?d=t";
            var uri = new Uri(url, UriKind.Absolute);

            // Adding today's date
            string data = "d_month=" + now.Month + "&d_day=" + now.Day + "&d_year=" + now.Year + "&view=View+Date";

            var wc = new WebClient();

            wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted);

            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            wc.Headers["Content-Length"] = data.Length.ToString();
            wc.UploadStringAsync(uri, "POST", data);
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
    }
}