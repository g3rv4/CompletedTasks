using System;
using System.Collections.Generic;

namespace CompletedTasks.Models.AirTable
{
    public class CompletedTask
    {
        public string Project { get; set; }
        public string Name { get; set; }
        public string EventData { get; set; }
        public string Url { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}