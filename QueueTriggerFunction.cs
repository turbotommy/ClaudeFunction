using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ClaudeFunction.Domain.Models;
using ClaudeFunction.Domain.Services;
using Newtonsoft.Json;

namespace ClaudeFunction
{
    public class QueueTriggerFunction
    {
        private readonly IMessageTransformationService _transformationService;
        private readonly ILogger<QueueTriggerFunction> _logger;

        public QueueTriggerFunction(
            IMessageTransformationService transformationService,
            ILogger<QueueTriggerFunction> logger)
        {
            _transformationService = transformationService ?? throw new ArgumentNullException(nameof(transformationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName("QueueTriggerFunction")]
        public void Run(
            [QueueTrigger("input-queue")] string myQueueItem,
            [Queue("output-queue")] out string outputQueueItem,
            ILogger log)
        {
            _logger.LogInformation($"Processing queue message: {myQueueItem}");

            try
            {
                // Deserialize input message
                var inputMessage = JsonConvert.DeserializeObject<QueueMessage>(myQueueItem);
                if (inputMessage == null)
                    throw new InvalidOperationException("Could not deserialize the queue message");

                // Transform message
                var transformedMessage = _transformationService.TransformMessage(inputMessage);

                // Serialize for output queue
                outputQueueItem = JsonConvert.SerializeObject(transformedMessage);

                _logger.LogInformation($"Message successfully transformed and sent to output queue");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                outputQueueItem = string.Empty; // We need to set this even in case of error
                throw; // Rethrow to mark the message as failed
            }
        }
    }
}
