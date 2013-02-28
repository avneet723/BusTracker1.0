using System;
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

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ITwitterAuthorizer auth = SharedState.Authorizer;

            if (auth == null || !auth.IsAuthorized)
            {
                NavigationService.Navigate(new Uri("/OAuth.xaml", UriKind.Relative));
            }
            else {
                var twitterCtx = new TwitterContext(auth);

                var queryResults =
                    from search in twitterCtx.Search
                    where search.Type == SearchType.Search &&
                          search.Query == "microsoft"
                    select search;

                display.Text = queryResults.Count().ToString();
                int index;
                for (index = 0; index < queryResults.Count(); index++)
                {
                    display.Text = queryResults.ElementAt(index).ToString();
                }
            }


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