using System;

namespace ClaudeFunction.Domain.Models
{
    public class QueueMessage
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
