using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Drawing;

namespace OsmDataFromMap

{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            //To Use Coordinate and a zoom level, read more at https://wiki.openstreetmap.org/wiki/Slippy_map_tilenames#Zoom_levels
            //Using Boundry Box
            await SendRequest(47.389704284254435, -122.05152509779643, 47.391717802883036, -122.04711567641095);
        }

        private static async Task SendRequest(double min_lat, double min_long, double max_lat, double max_lon)
        {
            var URL = string.Format("http://overpass-api.de/api/interpreter?data=[out:json];(node({0},{1},{2},{3});<;);out meta;",min_lat, min_long, max_lat, max_lon);
            Console.WriteLine(URL);
            var stringTask = client.GetStringAsync(URL);
            var msg = await stringTask;
            var json = JsonConvert.DeserializeObject<OSMdata>(msg);
            Console.WriteLine(msg);
            Console.WriteLine(json.elements.Length);
        }
    }
}
