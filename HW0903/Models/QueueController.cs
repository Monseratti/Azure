using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Text.Json;

namespace HW0903.Models
{
    public static class QueueController
    {
        static string clientName = "auction-lots";
        static string conn = "DefaultEndpointsProtocol=https;AccountName=monseratti1;AccountKey=3cxzc7TR780sOj74PYUAh2VkhU5Js3odAUykJZ7Ke1wGTYzunIG776TgYxH7b/L5Ujo58V0jAigb+AStSI9gTg==;EndpointSuffix=core.windows.net";
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

        public static async Task AddLot(Lot lot)
        {
            var msg = JsonSerializer.Serialize(lot);
            await (await GetQueueClient()).SendMessageAsync(msg, timeToLive: TimeSpan.FromHours(1));
        }

        public static async Task<PeekedMessage[]> ReceiveLots()
        {
            return (await (await GetQueueClient()).PeekMessagesAsync(maxMessages: 10)).Value;
        }

        public static async Task DeleteLot(string msgID)
        {
            var msgs = (await (await GetQueueClient()).ReceiveMessagesAsync(maxMessages:10, visibilityTimeout: TimeSpan.FromSeconds(1))).Value;
            var msg = msgs.Where(ms=>ms.MessageId.Equals(msgID)).FirstOrDefault();
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
