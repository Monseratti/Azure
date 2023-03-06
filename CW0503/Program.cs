// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;

var conn = "DefaultEndpointsProtocol=https;AccountName=monseratti;AccountKey=7Wmx4jeBwJsEXYPMwDUddz/BuiGsqxwkdZTjl6VkGPavHXELcZ0lg+iIor8pR/Q8W/BWWOkPUHY6+AStwazEUg==;EndpointSuffix=core.windows.net";
BlobServiceClient bsc = new BlobServiceClient(conn);

var containerName = $"container{Guid.NewGuid()}";
var localPath = "data";
Directory.CreateDirectory(localPath);
var fileName = "test.json";

await File.WriteAllTextAsync(Path.Combine(localPath, fileName), 
	"{\"name\":\"Hello\"}");

BlobContainerClient bcc = await bsc.CreateBlobContainerAsync(containerName);

BlobClient bc = bcc.GetBlobClient(fileName);

await bc.UploadAsync(Path.Combine(localPath, fileName));

await foreach(var file in bcc.GetBlobsAsync())
{
    Console.WriteLine(file.Name);
}
