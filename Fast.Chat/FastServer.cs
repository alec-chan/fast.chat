using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Geocoding.Google;
using System.IO;
using System.Reflection;
using Geocoding;

namespace Fast.Chat
{
    class FastServer
    {
        public static Dictionary<GoogleAddress, FastRoom> roomDict;
        private static HttpServer httpsv;

        public static void Start()
        {
            int port = 8080;
            httpsv = new HttpServer(port);

            // Set the document root path.
            httpsv.RootPath = "../../Fast.Chat.Frontend";

            // Set the HTTP GET request event.
            httpsv.OnGet += (sender, e) =>
            {
                var req = e.Request;
                var res = e.Response;

                var path = req.RawUrl;

                if(path == "/")
                {
                    path += "index.html";
                    
                    FastRoom r = new FastRoom(g);
                    FastService f = new FastService(r, g);


                    var contents = httpsv.GetFile(path);

                    if (path.EndsWith(".html"))
                    {
                        res.ContentType = "text/html";
                        res.ContentEncoding = Encoding.UTF8;
                    }
                    else if (path.EndsWith(".js"))
                    {
                        res.ContentType = "application/javascript";
                        res.ContentEncoding = Encoding.UTF8;
                    }

                    res.WriteContent(contents);



                }

            };

            httpsv.OnPost += (sender, e) =>
            {
                var req = e.Request;
                var res = e.Response;

                var path = req.RawUrl;

                APIParser.ExecuteAPIEndpoint(req.Headers["x-api-endpoint"], req.Headers["x-api-data"], )
            };

            httpsv.Start();

        }

        public static void Stop()
        {
            httpsv.Stop();
        }
    }
}
