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
    public partial class Dining : PhoneApplicationPage
    {
        public Dining()
        {
            InitializeComponent();
            Set_Open_Close();
        }

        public void TurnerPlace_Click(object sender, RoutedEventArgs e)
        {     
            NavigationService.Navigate(new Uri("/DiningInfoList.xaml", UriKind.Relative));
        }

        public void WestEnd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        public void HokieGrill_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DiningInfoList.xaml", UriKind.Relative));
        }

        public void Dietrick_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        public void Set_Open_Close()
        {
            DateTime now = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("Hour: " + now.Hour + " Minutes: " + now.Minute + " Seconds: " + now.Second);
        }

        private void Owens_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        private void DietrickExpress_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        private void Squires_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/DiningInfoList.xaml", UriKind.Relative));
        }

        private void Deets_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }
    }
}