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
        HtmlAgilityPack.HtmlDocument HD;
        String[] turnerLocations = { "1872 Fire Grill", "Atomic Pizzeria", "Bruegger's Bagels", "Dolci e Caffe", "Jamba Juice", 
                                       "Origami Grill", "Origami Sushi", "Qdoba Mexican Grill", "Soup Garden" };
        String[] squiresLocations = { "Sbarro", "Au Bon Pain - Squires Cafe", "Au Bon Pain - Squires Kiosk" };

        public DiningInfoList()
        {
            InitializeComponent();
            this.dateTime = DateTime.Now;

            // Load the Dining Hall that was saved into Current.State from Dining.xaml.cs
            this.location = PhoneApplicationService.Current.State["hall"].ToString();
            diningTitle.Title = location;

            if (location.Contains("Turner"))
            {
                placepicker.ItemsSource = turnerLocations;
            }
            else if (location.Contains("Squires"))
            {
                placepicker.ItemsSource = squiresLocations;
            }

            this.HD = ((App)Application.Current).HD;
            parseInfo(HD);
        }

        public DateTime dateTime { get; set; }

        /**
         * Performs the parsing of the dining hours information.
         */
        private void parseInfo(HtmlAgilityPack.HtmlDocument HD)
        {

            try // In case there is no available internet connection
            {
                bool diningHallFound = false;

                location = placepicker.SelectedItem.ToString();
                System.Diagnostics.Debug.WriteLine("Location: " + location);

                foreach (var node in HD.DocumentNode.SelectNodes("//body//li")) // Examine each node ...
                {
                    var array = node.InnerText.Split(new[] { '\r', '\n' });

                    var hours = "";

                    if (node.InnerText.Contains(location)) // ... for the dining hall we want
                    {
                        hours = array[2]; // Hours are in the Second Index
                        Regex r = new Regex(@"[\s]{2,}"); // Breakfast 7am-2pm              Lunch 2pm-5pm       Dinner 6pm-7pm    (There is a lot of Whitespace between each word)
                        string[] timesSplit = r.Split(hours); // [Breakfast 7am-2pm] [Lunch 2pm-5pm] [Dinner 6pm-7pm] (We have created a string array and the Whitespace has been removed)

                        if (!diningHallFound && node.InnerText.Contains("Breakfast") && node.InnerText.Contains("Lunch/Dinner") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {

                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1].Replace("Breakfast", ""); // Breakfast Hours
                            hour2.Text = "Lunch/Dinner Hours"; block2.Text = timesSplit[2].Replace("Lunch/Dinner", ""); // Lunch/Dinner Hours
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;
                        }

                        else if (!diningHallFound && node.InnerText.Contains("Regular Hours") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {

                            hour1.Text = "Regular Hours"; block1.Text = timesSplit[1].Replace("Regular Hours", ""); // Regular Hours
                            hour2.Text = ""; block2.Text = "";
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;
                        }

                        else if (!diningHallFound && node.InnerText.Contains("Lunch") && node.InnerText.Contains("Dinner") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {

                            hour1.Text = "Lunch"; block1.Text = timesSplit[1].Replace("Lunch", ""); ; // Lunch
                            hour2.Text = "Dinner"; block2.Text = timesSplit[2].Replace("Dinner", ""); // Dinner
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;
                        }

                        else if (!diningHallFound && node.InnerText.Contains("Breakfast") && node.InnerText.Contains("Lunch") && node.InnerText.Contains(placepicker.SelectedItem.ToString()))
                        {
                            hour1.Text = "Breakfast Hours"; block1.Text = timesSplit[1].Replace("Breakfast", ""); // Breakfast Hours
                            hour2.Text = "Lunch"; block2.Text = timesSplit[2].Replace("Lunch", ""); ; // Lunch
                            hour3.Text = ""; block3.Text = "";
                            diningHallFound = true;
                        }
                    }

                }

                if (!diningHallFound)
                {
                    hour1.Text = "Closed!"; block1.Text = "Closed!"; // No Dining Halls are Open
                    hour2.Text = ""; block2.Text = "";
                    hour3.Text = ""; block3.Text = "";
                }

            }

            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void date_change(object sender, Microsoft.Phone.Controls.DateTimeValueChangedEventArgs e)
        {
            //this.dateTime.Year = datepicker.Value.
            //System.Diagnostics.Debug.WriteLine(sender.GetType());
            //System.Diagnostics.Debug.WriteLine(dateTime);
            if (datepicker != null)
            {
                //datepicker = (DatePicker)sender;
                dateTime = (DateTime)datepicker.Value;
                System.Diagnostics.Debug.WriteLine(dateTime);
                ((App)Application.Current).download(dateTime);
                this.HD = ((App)Application.Current).HD;
                parseInfo(HD);
            }
        }

        private void hall_change(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            parseInfo(HD);
        }
    }
}