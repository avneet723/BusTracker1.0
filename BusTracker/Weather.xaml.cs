using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;

namespace BusTracker
{
    public partial class Weather : PhoneApplicationPage
    {
        private City currentCity;
        private City blacksburg;

        public Weather()
        {
            blacksburg = new City("Blacksburg", "24060", 37.2294, 80.4142);
            InitializeComponent();
        }

        // Event handler to handle when this page is navigated to
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {  
            if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            {
                // The user has opted out of Location.
                return;
            }

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            Geoposition geoposition = null;

            geoposition = await geolocator.GetGeopositionAsync( maximumAge : TimeSpan.FromMinutes(5), 
                timeout : TimeSpan.FromSeconds(10));

            if (geoposition != null)
            {
                currentCity.Latitude = geoposition.Coordinate.Latitude;
                currentCity.Longitude = geoposition.Coordinate.Longitude;
                currentCity.CityName = geoposition.CivicAddress.City;
                currentCity.ZipCode = geoposition.CivicAddress.PostalCode;
            }
            else { 
                // Handle timeout error
            }
        }
    }
}