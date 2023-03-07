// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;

var conn = Environment.GetEnvironmentVariable("MONSERATTI_CONN_STR");

BlobServiceClient blobClient = new BlobServiceClient(conn);

var contName = "con-monseratti";

var blobContainerClient = blobClient.GetBlobContainerClient(contName);

await foreach (var blob in blobContainerClient.GetBlobsAsync())
{
    Console.WriteLine(blob.Name);
    Console.WriteLine("__________________");
    Console.WriteLine(blob.Properties.LastModified);
    var thisBlobClient = blobContainerClient.GetBlobClient(blob.Name);
    Console.WriteLine(thisBlobClient.Uri.AbsoluteUri);
	Console.WriteLine("__________________");
}


