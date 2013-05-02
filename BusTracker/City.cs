using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracker
{
    class City : INotifyPropertyChanged 
    {
        private string cityName;
        private string zipCode;
        private double latitude;
        private double longitude;
        public event PropertyChangedEventHandler PropertyChanged;

        public City(String cityName, String zipCode, double latitude, double longitude) {
            this.cityName = cityName;
            this.zipCode = zipCode;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public String CityName {
            get
            {
                return cityName;
            }
            set
            {
                if (value != cityName)
                {
                    cityName = value;
                    NotifyPropertyChanged("CityName");
                }
            }
        }

        public String ZipCode {
            get {
                return zipCode;
            }

            set {
                if (value != zipCode) {
                    zipCode = value;
                    NotifyPropertyChanged("ZipCode");
                }
            }
        
        }

        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                if (value != latitude)
                {
                    latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                if (value != longitude)
                {
                    longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }
    }
}
