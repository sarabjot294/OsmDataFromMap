using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using JsonSubTypes;

namespace OsmDataFromMap
{
    // For More Reference of this class go to https://stackoverflow.com/questions/56284030/deserialize-open-street-map-json-in-c-sharp

    public class OSMdata
    {
        public float version { get; set; }
        public string generator { get; set; }
        public Osm3s osm3s { get; set; }

        // for arrays or collection this line must be present here
        [JsonConverter(typeof(JsonSubtypes), "type")]
        public Element[] elements { get; set; }
    }

    // no need to inherits from OSMData
    public class Osm3s
    {
        public DateTime timestamp_osm_base { get; set; }
        public string copyright { get; set; }
    }


    [JsonConverter(typeof(JsonSubtypes), "type")]
    [JsonSubtypes.KnownSubType(typeof(Node), "node")]
    [JsonSubtypes.KnownSubType(typeof(Edge), "way")]
    public abstract class Element : OSMdata
    {
        public abstract string type { get; }
    }

    public class Node : Element
    {
        public override string type { get; } = "node";
        public long id { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public NodeTags tags { get; set; }
    }

    public class NodeTags
    {
        public string highway { get; set; }
        public string _ref { get; set; }
    }

    public class Edge : Element
    {
        public override string type { get; } = "way";
        public long id { get; set; }
        public long[] nodes { get; set; }
        public EdgeTags tags { get; set; }
    }

    public class EdgeTags
    {
        public string highway { get; set; }
        public string name { get; set; }
        public string cfcc { get; set; }
        public string county { get; set; }
        public string oneway { get; set; }
    }
}
