using Geocoding.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.Chat
{
    class APIParser
    {
        static Dictionary<string, Func<string,FastService, string>> apiDict;
        
        static APIParser()
        {
            apiDict = new Dictionary<string, Func<string, FastService ,string>>();

            RegisterAPIEndpoint("SetLocation", SetLocationEndpoint);
        }

        public static string ExecuteAPIEndpoint(string endpoint, string data, FastService f)
        {
            if (apiDict[endpoint] != null)
            {
                var act = apiDict[endpoint];
                return act.Invoke(data, f);
            }
            else
            {
                return "";
            }
        }

        public static void RegisterAPIEndpoint(string endpoint, Func<string,FastService, string> callback)
        {
            if (!apiDict.ContainsKey(endpoint))
            {
                apiDict.Add(endpoint, callback);
            }
            else
            {
                Console.WriteLine("Cannot bind multiple callbacks to one command type...");
            }
        }


        public static string SetLocationEndpoint(string data, FastService f)
        {
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(data);
            if (obj.latitude != null && obj.longitude != null)
            {
                f.address = (GoogleAddress)AsyncHelpers.RunSync(() => Geocoder.ParseAddress((double)obj.longitude, (double)obj.latitude));
                return f.address.FormattedAddress;
            }
            else
            {
                return "";
            }
        }


    }
}
