using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracker
{
    class ForecastPeriod : INotifyPropertyChanged
    {
        private string timeName;
        private int highTemp;
        private int lowTemp;
        //private int chancePrecipitation;
        //sprivate string weatherType;
        private string textForecast;
        private string conditionIcon;
        public event PropertyChangedEventHandler PropertyChanged;

        public string TimeName
        {
            get
            {
                return timeName;
            }
            set
            {
                if (value != timeName)
                {
                    this.timeName = value;
                    NotifyPropertyChanged("TimeName");
                }
            }
        }

        public int HighTemp
        {
            get
            {
                return highTemp;
            }
            set
            {
                if (value != highTemp)
                {
                    this.highTemp = value;
                    NotifyPropertyChanged("HighTemp");
                }
            }
        }

        public int LowTemp
        {
            get
            {
                return lowTemp;
            }
            set
            {
                if (value != lowTemp)
                {
                    this.lowTemp = value;
                    NotifyPropertyChanged("LowTemp");
                }
            }
        }

        public string TextForecast
        {
            get
            {
                return textForecast;
            }
            set
            {
                if (value != textForecast)
                {
                    this.textForecast = value;
                    NotifyPropertyChanged("TextForecast");
                }
            }
        }

        public string ConditionIcon
        {
            get
            {
                return conditionIcon;
            }
            set
            {
                if (value != conditionIcon)
                {
                    this.conditionIcon = value;
                    NotifyPropertyChanged("ConditionIcon");
                }
            }
        }

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
