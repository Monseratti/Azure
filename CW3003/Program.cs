using CW3003;
using System.Text;
using System.Text.Json;

string _key = "c0a0dc6e56e042baa0b0765815956427";
string _endpoint = "https://api.cognitive.microsofttranslator.com/";
string inputStr = "You are really cool programmers and I hope that you'll" +
    " do your fucking best to realize all your potential!";
using (var _client = new HttpClient())
{
    using (HttpRequestMessage _message = new HttpRequestMessage())
    {
        _message.Method = HttpMethod.Post;
        _message.RequestUri = new Uri($"{_endpoint}translate?api-version=3.0&to=pl&to=uk&to=bg&to=he");
        _message.Headers.Add("Ocp-Apim-Subscription-Key", _key);
        _message.Headers.Add("Ocp-Apim-Subscription-Region", "westeurope");

        object[] _body = new object[] {
            new { Text = inputStr }
        };

        string request = JsonSerializer.Serialize(_body);
        _message.Content = new StringContent(request, Encoding.Unicode, "application/json");
        
        HttpResponseMessage _response= await _client.SendAsync(_message).ConfigureAwait(false);
        List<Root> roots = JsonSerializer.Deserialize<List<Root>>(await _response.Content.ReadAsStringAsync());

        foreach (var tr in roots)
        {
            Console.WriteLine($"Language:{tr.detectedLanguage.language}, score: {tr.detectedLanguage.score}");
            foreach (var transl in tr.translations)
            {
                Console.Write($"Translate to: {transl.to}\n");
                Console.Write($"Result: {transl.text}\n");
            }
        }
    }
}