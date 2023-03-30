using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace HW0703.Models
{
    public static class AIAnalyze
    {
        private static readonly string _endpoint = "https://monseratti-eyes.cognitiveservices.azure.com/";
        private static readonly string _key = "d7bcd28bf70540848ac83a06338e4380";
        private static readonly List<VisualFeatureTypes?> _visualTypes = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes?>().ToList();
        private static readonly List<VisualFeatureTypes?> _visualTags = new List<VisualFeatureTypes?>() { VisualFeatureTypes.Tags };
        private static ComputerVisionClient _client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_key))
        {
            Endpoint = _endpoint
        };

        public static async Task<string> MyAnalyseImageAsync(string uri)
        {
            string result = await AnalyseByDescriptionAsync(uri);
            if (!result.Equals(string.Empty)) return result;

            result = await AnalyseByTagsAsync(uri);
            if (!result.Equals(string.Empty)) return result;

            result = await AnalyseByCategories_Landmarks_Async(uri);
            if (!result.Equals(string.Empty)) return result;

            result = await AnalyseByCategories_Celebrities_Async(uri);
            if (!result.Equals(string.Empty)) return result;

            result = "unknown";
            return result;
        }

        private static async Task<string> AnalyseByDescriptionAsync(string uri)
        {
            string result = "";
            try
            {
                ImageAnalysis _analysis = await _client.AnalyzeImageAsync(uri, _visualTypes);
                foreach (var caption in _analysis.Description.Captions)
                {
                    result += $"{caption.Text}; ";
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        private static async Task<string> AnalyseByTagsAsync(string uri)
        {
            string result = "";
            try
            {
                ImageAnalysis _analysis = await _client.AnalyzeImageAsync(uri, _visualTags);
                foreach (var tag in _analysis.Tags)
                {
                    result += $"{tag.Name}; ";
                }
            }
            catch (Exception)
            {
            }

            return result;
        }
        private static async Task<string> AnalyseByCategories_Landmarks_Async(string uri)
        {
            string result = "";
            try
            {
                ImageAnalysis _analysis = await _client.AnalyzeImageAsync(uri, _visualTypes);
                foreach (var category in _analysis.Categories)
                {
                    if (category.Detail?.Landmarks != null)
                    {
                        result += $"{category.Name}; ";
                    }
                }
            }
            catch (Exception)
            {
            }

            return result;
        }
        private static async Task<string> AnalyseByCategories_Celebrities_Async(string uri)
        {
            string result = "";
            try
            {
                ImageAnalysis _analysis = await _client.AnalyzeImageAsync(uri, _visualTypes);
                foreach (var category in _analysis.Categories)
                {
                    if (category.Detail?.Celebrities != null)
                    {
                        result += $"{category.Name}; ";
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        
        public static async Task<bool> AnalyseByAdultContent(string uri)
        {
            bool result = false;
            try
            {
                ImageAnalysis _analysis = await _client.AnalyzeImageAsync(uri, _visualTypes);
                result = _analysis.Adult.IsAdultContent;
                if (!result) result = _analysis.Adult.IsRacyContent;
                if (!result) result = _analysis.Adult.IsGoryContent;
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static async Task<string> ReadTextFromPicture(string uri)
        {
            string text = "";
            var _textAnalyze = await _client.RecognizePrintedTextAsync(true, uri);
            foreach (var _textregion in _textAnalyze.Regions)
            {
                foreach (var _line in _textregion.Lines)
                {
                    foreach (var word in _line.Words)
                    {
                        text += $"{word.Text} ";
                    }
                    text += "\r\n";
                }
                text += "\r\n";
            }
            return text;
        }
    }
}
