using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HW3003.Models
{
    public static class TranslateController
    {
        static string _key = "c0a0dc6e56e042baa0b0765815956427";
        static string _endpoint = "https://api.cognitive.microsofttranslator.com/";
        static string _region = "westeurope";

        public static async Task<string> TranslateAsync(string from, string to, string text)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                using (HttpRequestMessage message = new HttpRequestMessage())
                {
                    message.Method = HttpMethod.Post;
                    message.RequestUri = new Uri($"{_endpoint}translate?api-version=3.0&{(from.Equals(string.Empty) ? "" : $"from={from}&")}to={to}");
                    message.Headers.Add("Ocp-Apim-Subscription-Key", _key);
                    message.Headers.Add("Ocp-Apim-Subscription-Region", _region);

                    object[] body = new object[] { new { Text = text } };

                    string request = JsonSerializer.Serialize(body);
                    message.Content = new StringContent(request, Encoding.Unicode, "application/json");

                    HttpResponseMessage _response = await client.SendAsync(message).ConfigureAwait(false);
                    var respText = await _response.Content.ReadAsStringAsync();
                    List<Root> roots = JsonSerializer.Deserialize<List<Root>>(respText);
                    foreach(var root in roots)
                    {
                        result = JsonSerializer.Serialize(
                            new 
                        { 
                            lang = root.DetectedLanguage?.Language??"", 
                            text = root.Translations[0].Text 
                        }
                        );
                    }
                }
            }
            return result;

        }

        //public static async Task<string> DetectAsync(string text)
        //{
        //    string type = "";
        //    using (var client = new HttpClient())
        //    {
        //        using (HttpRequestMessage message = new HttpRequestMessage())
        //        {
        //            message.Method = HttpMethod.Post;
        //            message.RequestUri = new Uri($"{_endpoint}detect?api-version=3.0");
        //            message.Headers.Add("Ocp-Apim-Subscription-Key", _key);
        //            message.Headers.Add("Ocp-Apim-Subscription-Region", _region);

        //            object[] body = new object[] { new { Text = text } };

        //            message.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.Unicode, "applicalion/json");

        //            HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false);

        //            List<DetectedLanguage> lang = JsonSerializer.Deserialize<List<DetectedLanguage>>(await response.Content.ReadAsStringAsync());

        //            type = lang.OrderBy(l => l.Score).Last().Language;
        //        }
        //    }
        //    return type;
        //}

        private class DetectedLanguage
        {
            [JsonPropertyName("language")]
            public string Language { get; set; }

            [JsonPropertyName("score")]
            public double Score { get; set; }

            //[JsonPropertyName("isTranslationSupported")]
            //public bool IsTranslationSupported { get; set; }

            //[JsonPropertyName("isTransliterationSupported")]
            //public bool IsTransliterationSupported { get; set; }
        }

        private class Root
        {
            [JsonPropertyName("detectedLanguage")]
            public DetectedLanguage DetectedLanguage { get; set; }

            [JsonPropertyName("translations")]
            public List<Translation> Translations { get; set; }
        }

        private class Translation
        {
            [JsonPropertyName("text")]
            public string Text { get; set; }

            [JsonPropertyName("to")]
            public string To { get; set; }
        }
    }
}
