using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CW3003
{
    public class DetectedLanguage
    {
        [JsonPropertyName("language")]
        public string language { get; set; }

        [JsonPropertyName("score")]
        public double score { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("detectedLanguage")]
        public DetectedLanguage detectedLanguage { get; set; }

        [JsonPropertyName("translations")]
        public List<Translation> translations { get; set; }
    }

    public class Translation
    {
        [JsonPropertyName("text")]
        public string text { get; set; }

        [JsonPropertyName("to")]
        public string to { get; set; }
    }
}
