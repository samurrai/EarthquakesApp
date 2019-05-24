using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthQuakesApp
{
    public class Info
    {
        [JsonProperty("mag")]
        public Double? Magnitude { get; set; }

        [JsonProperty("dmin")]
        public double? Depth { get; set; }

        public string Place { get; set; }

        public string Time { get; set; }

    }
}
