using System;
using ClaudeFunction.Domain.Models;

namespace ClaudeFunction.Domain.Services
{
    public class MessageTransformationService : IMessageTransformationService
    {
        public QueueMessage TransformMessage(QueueMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return new QueueMessage
            {
                Id = message.Id,
                Content = message.Content.ToUpper(), // Simple transformation - converts content to upper case
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
