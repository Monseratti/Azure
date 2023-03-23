using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW2003_1
{
    public static class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("test-queue")] string message, ILogger log)
        {
            log.LogInformation(message);
        }
    }
}
