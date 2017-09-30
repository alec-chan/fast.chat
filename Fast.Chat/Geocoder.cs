using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geocoding;
using Geocoding.Google;

namespace Fast.Chat
{
    public class Geocoder
    {
        static GoogleGeocoder geocoder;

        static Geocoder()
        {
            geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyCmsW-KsXCLqOZifa0Hwz7jFH3IqfG5CtI" };
        }

        public static async Task<Address> ParseAddress(string address)
        {
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(address);

            return addresses.First();
        }

        public static async Task<Address> ParseAddress(double lat, double lon)
        {
            IEnumerable<Address> ads = await geocoder.ReverseGeocodeAsync(new Location(lat, lon));

            return ads.First();
        }
    }
}
