using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

var _endPoint = "https://monseratti-eyes.cognitiveservices.azure.com/";
var _key = "d7bcd28bf70540848ac83a06338e4380";

var _pictureList = new List<string>()
{
	"https://tied.verbix.com/project/script/cyril.gif",
	//"https://i.cbc.ca/1.6504994.1656471758!/cpImage/httpImage/image.jpg_gen/derivatives/16x9_780/2021-met-museum-costume-institute-benefit-gala.jpg",
	//"https://www.instyle.com/thmb/wBYg6NN17U_gfORuGAnCMvnZ4EI=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/050317-salma-hayek-d634671037d74a489ae2f65ee27ecb85.jpg",
	//"https://static6.depositphotos.com/1003369/659/i/600/depositphotos_6591667-stock-photo-close-up-of-beautiful-womanish.jpg",
	//"https://st2.depositphotos.com/1606449/8096/i/600/depositphotos_80968814-stock-photo-cat-looking-into-mirror-and.jpg"
};

ComputerVisionClient _client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_key))
{
	Endpoint = _endPoint
};

List<VisualFeatureTypes?> _visuals = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes?>().ToList();
//List<VisualFeatureTypes?> _visuals = new List<VisualFeatureTypes?>() { VisualFeatureTypes.Tags };

foreach (var picture in _pictureList)
{

	ImageAnalysis _analysis = await _client.AnalyzeImageAsync(picture, _visuals);

	//foreach (var result in _analysis.Description.Captions)
	//{
	//	Console.WriteLine($"{result.Text}\t{result.Confidence}");
	//}
	//foreach (var result in _analysis.Tags)
	//{
	//	Console.WriteLine($"{result.Name}\t{result.Confidence}\t{result.Hint}");
	//}
	//foreach (var result in _analysis.Faces)
	//{
	//	Console.WriteLine($"{result.Age}\t{result.Gender}\t{result.FaceRectangle.Top} {result.FaceRectangle.Top}");
	//}
	//Console.WriteLine(_analysis.Adult.IsAdultContent);
	//Console.WriteLine(_analysis.Adult.AdultScore);
	//Console.WriteLine(_analysis.Adult.IsRacyContent);
	//Console.WriteLine(_analysis.Adult.RacyScore);
	//Console.WriteLine(_analysis.Adult.IsGoryContent);
	//Console.WriteLine(_analysis.Adult.GoreScore);
	//Console.WriteLine($"{_analysis.Color.DominantColorForeground}, " +
	//	$"{_analysis.Color.DominantColorBackground}, " +
	//	$"{string.Join(", ", _analysis.Color.DominantColors)}, " +
	//	$"{_analysis.Color.IsBWImg}, ");
	//foreach (var result in _analysis.Categories)
	//{
	//	if (result.Detail?.Landmarks != null)
	//	{
	//		foreach (var item in result.Detail.Landmarks)
	//		{
	//			Console.WriteLine($"Landmarks: {item.Name}\t{item.Confidence}");
	//		}
	//	}
	//	if (result.Detail?.Celebrities != null)
	//	{
	//		foreach (var item in result.Detail.Celebrities)
	//		{
	//			Console.WriteLine($"Celebrities: {item.Name}\t{item.Confidence}");
	//		}
	//	}
	//}
	var _textAnalyze = await _client.RecognizePrintedTextAsync(true, picture);
	foreach (var _textregion in _textAnalyze.Regions)
	{
		foreach (var _line in _textregion.Lines)
		{
			foreach(var word in _line.Words)
			{
                Console.Write($"{word.Text} ");
            }
            Console.WriteLine();
        }
        Console.Write("\n\n");
    }
}