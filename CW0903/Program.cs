// See https://aka.ms/new-console-template for more information

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

var conn = "DefaultEndpointsProtocol=https;AccountName=monseratti1;AccountKey=3cxzc7TR780sOj74PYUAh2VkhU5Js3odAUykJZ7Ke1wGTYzunIG776TgYxH7b/L5Ujo58V0jAigb+AStSI9gTg==;EndpointSuffix=core.windows.net";

QueueServiceClient queue =  new QueueServiceClient(conn);

QueueClient client = queue.GetQueueClient("myqueue");

foreach (var item in (await client.ReceiveMessagesAsync(maxMessages:1)).Value)
{
    //Console.WriteLine(item.MessageId);
    //Console.WriteLine(item.InsertedOn);
    //Console.WriteLine(item.ExpiresOn);
    //Console.WriteLine(item.Body.ToString());
    //Console.WriteLine(item.DequeueCount);
    await client.DeleteMessageAsync(item.MessageId, item.PopReceipt);
}
//acf06935-f479-42b4-8a5c-52fffca2aba8
//await client.SendMessageAsync("Hello another World");

//QueueClient client1 = queue.GetQueueClient("myqueue");

//await client1.DeleteIfExistsAsync();