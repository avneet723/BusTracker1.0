using System;
using System.IO;
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
    public partial class DiningInfo : PhoneApplicationPage
    {
        String location;
        HtmlAgilityPack.HtmlDocument HD;

        public DiningInfo()
        {
            InitializeComponent();

            this.dateTime = DateTime.Now;

            String diningHall = PhoneApplicationService.Current.State["hall"].ToString(); // Load the Dining Hall that was saved into Current.State from Dining.xaml.cs

            diningTitle.Title = diningHall; // Set the Title to the Dining Hall
            var splitstr = diningHall.Split();
            location = splitstr[0]; // Searching for "Turner" is easier than searching for "Turner Place"
            
            this.HD = ((App)Application.Current).HD;
            parseInfo(HD);
        }

        public DateTime dateTime { get; set; }

        private void parseInfo(HtmlAgilityPack.HtmlDocument HD)
        {

            try // In case there is no available internet connection
            {
                bool diningHallFound = false;

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

                            hour1.Text = "Brunch Hours"; block1.Text = timesSplit[1].Replace("Brunch", ""); // Brunch Hours
                            hour2.Text = "Regular Hours"; block2.Text = timesSplit[2].Replace("Regular Hours", ""); // Regular Hours
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Lunch/Dinner")) // Fire Grill at Turner Place During Weekday
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1].Replace("Breakfast", ""); // Breakfast Hours
                            hour2.Text = "Lunch/Dinner Hours"; block2.Text = timesSplit[2].Replace("Lunch/Dinner", ""); // Lunch/Dinner Hours
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Breakfast") && node.InnerText.Contains("Dinner")) // D2 During Weekday
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1].Replace("Breakfast", ""); // Breakfast Hours
                            hour2.Text = "Lunch Hours"; block2.Text = timesSplit[2].Replace("Lunch", ""); // Lunch Hours
                            hour3.Text = "Dinner Hours"; block3.Text = timesSplit[3].Replace("Dinner", ""); // Dinner Hours
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Breakfast") && node.InnerText.Contains("Lunch")) // Vet Med Cafe During Weekday and Break
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1].Replace("Breakfast", ""); // Breakfast Hours
                            hour2.Text = "Lunch Hours"; block2.Text = timesSplit[2].Replace("Lunch", ""); // Lunch Hours
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Reservation")) // Origami Grill at Turner Place During Weekday
                        {

                            hour1.Text = "Lunch Hours"; block1.Text = timesSplit[1].Replace("Lunch", ""); // Lunch Hours
                            hour2.Text = "Dinner Hours (Reservation Required)"; block2.Text = timesSplit[2].Replace("Dinner", ""); // Dinner Hours
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Special")) // ABP During Break
                        {

                            hour1.Text = "Special Hours"; block1.Text = timesSplit[1].Replace("Special", ""); // Special Hours
                            hour2.Text = ""; block2.Text = "";
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;

                        }

                        else if (!diningHallFound && node.InnerText.Contains("Regular")) // Any Other Time
                        {

                            hour1.Text = "Regular Hours"; block1.Text = timesSplit[1].Replace("Regular Hours", ""); // Regular Hours
                            hour2.Text = ""; block2.Text = "";
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;

                        }

                    }

                }

                if (!diningHallFound)
                {
                    hour1.Text = "Closed!"; block1.Text = ""; // No Dining Halls are Open
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void date_change(object sender, Microsoft.Phone.Controls.DateTimeValueChangedEventArgs e)
        {
            //Update HTML and reparse
            ((App)Application.Current).download(dateTime);
            System.Diagnostics.Debug.WriteLine(dateTime);
            this.HD = ((App)Application.Current).HD;
            parseInfo(HD);
        }
    }
}