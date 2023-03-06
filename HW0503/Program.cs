// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;

var conn = "DefaultEndpointsProtocol=https;AccountName=monseratti;AccountKey=7Wmx4jeBwJsEXYPMwDUddz/BuiGsqxwkdZTjl6VkGPavHXELcZ0lg+iIor8pR/Q8W/BWWOkPUHY6+AStwazEUg==;EndpointSuffix=core.windows.net";

var localPath = "savedata";
Directory.CreateDirectory(localPath);

BlobContainerClient containerClient = new BlobContainerClient(conn, "containerac4c92d0-0d36-4876-91e1-006fbac00f3c");

BlobClient blobClient = containerClient.GetBlobClient("test.json");
await blobClient.DownloadToAsync(localPath);

Console.WriteLine(File.ReadAllText($"{localPath}\\test.json"));
Console.ReadLine();