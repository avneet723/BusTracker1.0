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

            DateTime now = DateTime.Now; // Use to Display Open or Close for a Dining Hall
        }

        public void TurnerPlace_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "Turner Place"; // Save "Turner Place" into Current.State
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative)); // Navigate to the DiningInfo Page
        }

        public void WestEnd_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "West End";
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        public void HokieGrill_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "Hokie Grill";
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        public void Dietrick_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "D2";
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        private void Owens_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "Owens Hall";
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        private void DietrickExpress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "DX";
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        private void Squires_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "Squires";
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }

        private void Deets_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["hall"] = "Deet's";
            NavigationService.Navigate(new Uri("/DiningInfo.xaml", UriKind.Relative));
        }
    }
}