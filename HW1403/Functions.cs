using Microsoft.Azure.WebJobs;

namespace HW1403
{
	public class Functions
	{
		public static void ProcessQueueMessage([QueueTrigger("orders")] string message, ILogger log)
		{
			log.LogInformation(message);
		}
	}
}
