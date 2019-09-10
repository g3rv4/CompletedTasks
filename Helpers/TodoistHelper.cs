using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CompletedTasks.Models.Todoist;
using Microsoft.Extensions.Configuration;

namespace CompletedTasks.Helpers
{
    public interface ITodoistHelper
    {
        Task<string> GetLabelNameAsync(long labelId);

        Task<string> GetProjectNameAsync(long projectId);
    }

    public class TodoistHelper : ITodoistHelper
    {
        private HttpClient httpClient { get; }

        private Dictionary<long, string> labelNames { get; set; }

        private Dictionary<long, string> projectNames { get; set; }

        public TodoistHelper(IConfiguration configuration)
        {
            var baseAddress = new Uri("https://api.todoist.com");
            httpClient = new HttpClient { BaseAddress = baseAddress };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + configuration.GetValue<string>("TODOIST_API_KEY"));
        }

        public async Task<string> GetLabelNameAsync(long labelId)
        {
            if (labelNames == null || !labelNames.TryGetValue(labelId, out var labelName))
            {
                var response = await httpClient.GetAsync("rest/v1/labels");
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    labelNames = Jil.JSON.Deserialize<Label[]>(reader).ToDictionary(l => l.Id, l => l.Name);
                }

                labelNames.TryGetValue(labelId, out labelName);
            }

            return labelName;
        }

        public async Task<string> GetProjectNameAsync(long projectId)
        {
            if (projectNames == null || !projectNames.TryGetValue(projectId, out var projectName))
            {
                var response = await httpClient.GetAsync("rest/v1/projects");
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    projectNames = Jil.JSON.Deserialize<Project[]>(reader).ToDictionary(l => l.Id, l => l.Name);
                }

                projectNames.TryGetValue(projectId, out projectName);
            }

            return projectName;
        }
    }
}