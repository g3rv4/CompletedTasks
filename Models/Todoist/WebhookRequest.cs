using System.Runtime.Serialization;

namespace CompletedTasks.Models.Todoist
{
    public class WebhookRequest
    {
        [DataMember(Name = "event_name")]
        public string EventName { get; set; }

        [DataMember(Name = "event_data")]
        public WebhookEventData EventData { get; set; }
    }
}