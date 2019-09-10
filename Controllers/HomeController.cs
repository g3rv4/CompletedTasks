using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using CompletedTasks.Helpers;
using System.Collections.Generic;
using CompletedTasks.Models.Todoist;

namespace CompletedTasks.Controllers
{
    public class HomeController : Controller
    {
        private ITodoistHelper _todoistHelper;
        private IAirtableHelper _airtableHelper;
        public HomeController(ITodoistHelper todoistHelper, IAirtableHelper airtableHelper)
        {
            _todoistHelper = todoistHelper;
            _airtableHelper = airtableHelper;
        }

        [Route("/todoist-event")]
        public async Task<IActionResult> Event()
        {
            WebhookRequest request;
            string body;
            using (var reader = new StreamReader(Request.Body))
            {
                body = reader.ReadToEnd();
                request = Jil.JSON.Deserialize<WebhookRequest>(body, Jil.Options.ISO8601);
            }

            var eventData = request.EventData;
            var labels = new List<string>();
            foreach (var labelId in eventData.LabelIds)
            {
                labels.Add(await _todoistHelper.GetLabelNameAsync(labelId));
            }
            var project = await _todoistHelper.GetProjectNameAsync(eventData.ProjectId);
            await _airtableHelper.InsertCompletedTaskAsync(project, eventData.Name, labels, eventData.CompletedDate, eventData.Url, body);

            return new EmptyResult();
        }
    }
}
