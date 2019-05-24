using Newtonsoft.Json;
using System.Collections.Generic;

namespace EarthQuakesApp
{
    public class Feature
    {
        [JsonProperty("Properties")]
        public Info Infos { get; set; }
    }
}