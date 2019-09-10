using System.Runtime.Serialization;

namespace CompletedTasks.Models.Todoist
{
    public class Label
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "order")]
        public int Order { get; set; }
    }
}