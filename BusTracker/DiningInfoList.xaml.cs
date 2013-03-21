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

        String location;

        public DiningInfoList()
        {
            InitializeComponent();

            this.dateTime = DateTime.Now;

            String diningHall = PhoneApplicationService.Current.State["hall"].ToString(); // Load the Dining Hall that was saved into Current.State from Dining.xaml.cs

            diningTitle.Title = diningHall; // Set the Title to the Dining Hall
            var splitstr = diningHall.Split();
            location = splitstr[0]; // Searching for "Turner" is easier than searching for "Turner Place"

            download(); // When the user navigates to this page, start downloading the times

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

            try // In case there is no available internet connection
            {
                bool diningHallFound = false;

                HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
                HD.LoadHtml(e.Result); // Load the HTML from e.Result which is currently a String

                foreach (var node in HD.DocumentNode.SelectNodes("//body//li")) // Examine each node ...
                {

                    var array = node.InnerText.Split(new[] { '\r', '\n' });

                    var hours = "";

                    if (node.InnerText.Contains(location)) // ... for the dining hall we want
                    {
                        hours = array[2]; // Hours are in the Second Index
                        Regex r = new Regex(@"[\s]{2,}"); // Breakfast 7am-2pm              Lunch 2pm-5pm       Dinner 6pm-7pm    (There is a lot of Whitespace between each word)
                        string[] timesSplit = r.Split(hours); // [Breakfast 7am-2pm] [Lunch 2pm-5pm] [Dinner 6pm-7pm] (We have created a string array and the Whitespace has been removed)

                        if (!diningHallFound && node.InnerText.Contains("Brunch")) // Owens on Saturday or D2, Owens, and West End on Sunday
                        {

                            hour1.Text = "Brunch Hours"; block1.Text = timesSplit[1]; // Brunch Hours
                            hour2.Text = "Regular Hours"; block2.Text = timesSplit[2]; // Regular Hours
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Lunch/Dinner")) // Fire Grill at Turner Place During Weekday
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1]; // Breakfast Hours
                            hour2.Text = "Lunch/Dinner Hours"; block2.Text = timesSplit[2]; // Lunch/Dinner Hours
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Breakfast") && node.InnerText.Contains("Dinner")) // D2 During Weekday
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1]; // Breakfast Hours
                            hour2.Text = "Lunch Hours"; block2.Text = timesSplit[2]; // Lunch Hours
                            hour3.Text = "Dinner Hours"; block3.Text = timesSplit[3]; // Dinner Hours
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Breakfast") && node.InnerText.Contains("Lunch")) // Vet Med Cafe During Weekday and Break
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1]; // Breakfast Hours
                            hour2.Text = "Lunch Hours"; block2.Text = timesSplit[2]; // Lunch Hours
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Reservation")) // Origami Grill at Turner Place During Weekday
                        {

                            hour1.Text = "Lunch Hours"; block1.Text = timesSplit[1]; // Lunch Hours
                            hour2.Text = "Dinner Hours (Reservation Required)"; block2.Text = timesSplit[2]; // Dinner Hours
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Special")) // ABP During Break
                        {

                            hour1.Text = "Special Hours"; block1.Text = timesSplit[1]; // Special Hours
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Regular")) // Any Other Time
                        {

                            hour1.Text = "Regular Hours"; block1.Text = timesSplit[1]; // Regular Hours
                            diningHallFound = true;

                        }

                    }

                }

                if (!diningHallFound)
                {
                    hour1.Text = "Closed"; block1.Text = "Closed"; // No Dining Halls are Open
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
    }
}