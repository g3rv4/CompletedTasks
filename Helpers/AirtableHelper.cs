using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompletedTasks.Models.AirTable;
using Microsoft.Extensions.Configuration;

namespace CompletedTasks.Helpers
{
    public interface IAirtableHelper
    {
        Task InsertCompletedTaskAsync(string project, string name, List<string> tags, DateTime completedDate, string url, string eventData);
    }
    public class AirtableHelper : IAirtableHelper
    {
        private HttpClient httpClient { get; }
        private string baseId { get; }

        public AirtableHelper(IConfiguration configuration)
        {
            var baseAddress = new Uri("https://api.airtable.com/v0");
            httpClient = new HttpClient { BaseAddress = baseAddress };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + configuration.GetValue<string>("AIRTABLE_API_KEY"));

            baseId = configuration.GetValue<string>("AIRTABLE_BASE_ID");
        }

        public async Task InsertCompletedTaskAsync(string project, string name, List<string> tags, DateTime completedDate, string url, string eventData)
        {
            var model = new CompletedTask
            {
                Project = project,
                Name = name,
                Tags = tags,
                CompletionDate = completedDate,
                Url = url,
                EventData = eventData,
            };

            var json = Jil.JSON.Serialize(new CreateCompletedTaskRequest(model), Jil.Options.ISO8601);

            using (var request = new HttpRequestMessage(HttpMethod.Post, $"v0/{baseId}/CompletedTasks"))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}