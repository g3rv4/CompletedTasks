using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CompletedTasks.Models.AirTable
{
    public class CreateCompletedTaskRequest
    {
        [DataMember(Name = "records")]
        public List<Record> Records { get; set; }

        [DataMember(Name = "typecast")]
        public bool Typecast { get; set; }

        public CreateCompletedTaskRequest() { }

        public CreateCompletedTaskRequest(List<CompletedTask> tasks)
        {
            Records = new List<Record>();
            foreach (var task in tasks)
            {
                Records.Add(new Record { Fields = task });
            }
            Typecast = true;
        }

        public CreateCompletedTaskRequest(CompletedTask task) : this(new List<CompletedTask> { task }) { }
    }

    public class Record
    {
        [DataMember(Name = "fields")]
        public CompletedTask Fields { get; set; }
    }
}