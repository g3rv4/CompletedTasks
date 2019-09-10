using System;
using System.Runtime.Serialization;

namespace CompletedTasks.Models.Todoist
{
    public class WebhookEventData
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "content")]
        public string Name { get; set; }

        [DataMember(Name = "project_id")]
        public long ProjectId { get; set; }

        [DataMember(Name = "labels")]
        public long[] LabelIds { get; set; }

        [DataMember(Name = "date_completed")]
        public DateTime CompletedDate { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}