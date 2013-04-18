using System;
using System.Xml;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BusTracker.Resources;
using LinqToTwitter;

namespace BusTracker
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static ITwitterAuthorizer Authorizer { get; set; }
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //feedBrowser.Navigate(new Uri("http://www.google.com"));
            feedBrowser.Navigate(new Uri("https://twitter.com/search?q=virginia%20tech&src=typd"));
            // Sample code to localize the ApplicationBar

        }
        
        private void button_click(object sender, RoutedEventArgs e)
        {
            ITwitterAuthorizer auth = SharedState.Authorizer;

            if (auth == null || !auth.IsAuthorized)
            {
                NavigationService.Navigate(new Uri("/OAuth.xaml", UriKind.Relative));
            }
            else
            {
                var twitterCtx = new TwitterContext(auth);
                
                Console.WriteLine(twitterCtx.User);

                var user =
                (from usr in twitterCtx.Search
                 where usr.Type == SearchType.Search &&
                       usr.Query == "Virginia Tech" &&
                       usr.Count == 7
                 select usr)
                 .SingleOrDefault();

                if (user != null)
                    System.Diagnostics.Debug.WriteLine("User Name: " + user.ToString());
                else
                {
                    System.Diagnostics.Debug.WriteLine("User Name: null");
                }
            // /
            }

        }

        private void Info_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}