using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Text.Json;

namespace HW1403.Models
{
	public static class QueueCT
	{
		static string clientName = $"orders";
		static string conn = "DefaultEndpointsProtocol=https;AccountName=monseratti2;AccountKey=H49BVQyDcdL1Y6eGcf8Z0DetN9k6z3slENeFd3jf88/3MoFbOXpznlOjZB8rzmTQdu4lXH8jDIWr+ASt7tYNPA==;EndpointSuffix=core.windows.net";
		static QueueServiceClient service = new QueueServiceClient(conn);

		static async Task<QueueClient> GetQueueClient()
		{
			try
			{
				return await service.CreateQueueAsync(clientName);
			}
			catch (Exception)
			{
				return service.GetQueueClient(clientName);
			}
		}

		public static async Task AddData(object data)
		{
			var msg = JsonSerializer.Serialize(data);
			await (await GetQueueClient()).SendMessageAsync(msg, timeToLive: new TimeSpan(0, 0, -1));
		}

		public static async Task<PeekedMessage[]> ReceiveData()
		{
			return (await (await GetQueueClient()).PeekMessagesAsync(maxMessages: 10)).Value;
		}

		public static async Task DeleteData(string msgID)
		{
			var msgs = (await (await GetQueueClient()).ReceiveMessagesAsync(maxMessages: 10, visibilityTimeout: TimeSpan.FromSeconds(1))).Value;
			var msg = msgs.Where(ms => ms.MessageId.Equals(msgID)).FirstOrDefault();
			try
			{
				await (await GetQueueClient()).DeleteMessageAsync(msg.MessageId, msg.PopReceipt);
			}
			catch (Exception)
			{
			}
		}
	}
}
