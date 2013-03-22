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
        String[] turnerLocations = { "1872 Fire Grill", "Atomic Pizzeria", "Bruegger's Bagels", "Dolci e Caffe", "Jamba Juice", 
                                       "Origami Grill", "Origami Sushi", "Qdoba Mexican Grill", "Soup Garden" };
        String[] squiresLocations = { "Sbarro", "Au Bon Pain Cafe", "Au Bon Pain Kiosk" };

        public DiningInfoList()
        {
            InitializeComponent();

            this.dateTime = DateTime.Now;

            String diningHall = PhoneApplicationService.Current.State["hall"].ToString(); // Load the Dining Hall that was saved into Current.State from Dining.xaml.cs

            diningTitle.Title = diningHall; // Set the Title to the Dining Hall
            var splitstr = diningHall.Split();
            location = splitstr[0]; // Searching for "Turner" is easier than searching for "Turner Place"
            //location = diningHall;
            download(); // When the user navigates to this page, start downloading the times

        }

        public DateTime dateTime { get; set; }

        /**
         * Handles the HTTP Post request and obtains the information from the secure.hosting.dining url 
         */
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


        /**
         * Performs the parsing of the dining hours information.
         */
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

                    if (node.InnerText.Contains(location) && location.Equals("Turner")) // ... for the dining hall we want
                    {
                        placepicker.ItemsSource = turnerLocations;
                        hours = array[2]; // Hours are in the Second Index
                        Regex r = new Regex(@"[\s]{2,}"); // Breakfast 7am-2pm              Lunch 2pm-5pm       Dinner 6pm-7pm    (There is a lot of Whitespace between each word)
                        string[] timesSplit = r.Split(hours); // [Breakfast 7am-2pm] [Lunch 2pm-5pm] [Dinner 6pm-7pm] (We have created a string array and the Whitespace has been removed)

                        if (!diningHallFound && node.InnerText.Contains("Breakfast") && node.InnerText.Contains("Lunch/Dinner") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1]; // Breakfast Hours
                            hour2.Text = "Lunch/Dinner Hours"; block2.Text = timesSplit[2]; // Lunch/Dinner Hours
                            hour3.Text = ""; block3.Text = ""; // Empty String
                            diningHallFound = true;
                        }

                        else if (!diningHallFound && node.InnerText.Contains("Regular Hours") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {

                            hour1.Text = "Regular Hours"; block1.Text = timesSplit[1]; // Regular Hours
                            hour2.Text = ""; block2.Text = ""; // Empty String
                            hour3.Text = ""; block3.Text = ""; // Empty String
                            diningHallFound = true;
                        }

                        else if (!diningHallFound && node.InnerText.Contains("Lunch") && node.InnerText.Contains("Dinner") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {

                            hour1.Text = "Lunch"; block1.Text = timesSplit[1]; // Lunch
                            hour2.Text = "Dinner"; block2.Text = timesSplit[2]; // Dinner
                            hour3.Text = ""; block3.Text = ""; // Empty String
                            diningHallFound = true;
                        }

                        else if (!diningHallFound && node.InnerText.Contains("Breakfast") && !diningHallFound && node.InnerText.Contains("Lunch") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {
                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1]; // Breakfast Hours
                            hour2.Text = ""; block2.Text = ""; // Empty String
                            hour3.Text = ""; block3.Text = ""; // Empty String
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

        private void hall_change(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            download();
        }
    }
}