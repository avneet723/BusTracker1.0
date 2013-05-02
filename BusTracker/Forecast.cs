using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracker
{
    class Forecast
    {
        private const string apiCode = "A4677192716";
        private const int unitType = 0; // 0 for US Standard, 1 for Metric
        private const string weatherBugForecastUrl = "http://api.wxbug.net/getForecastRSS.aspx?ACode={0}&zipCode={1}&unittype={2}";

        public Forecast() 
        {
 
        }

        private String buildURL(City city)
        {
            if (city == null)
                return null;
            if (city.CityName != null && city.ZipCode != null && city.ZipCode != "")
            {
                return String.Format(weatherBugForecastUrl, apiCode, city.ZipCode, unitType);
            }
            else {
                // This could probably be handled better
                return null;
            }
        }
    }
}
