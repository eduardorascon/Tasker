using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataServices.JIRA
{
    public class JiraService
    {
        /// <summary>
        /// Return a Issue detailed 
        /// </summary>
        /// <param name="key">The Issue number or item showed in the JIRAS Web UI</param>
        /// <returns></returns>
        public IssueDTO GetIssue(string key)
        {
            IssueDTO returnObject;
            var client = new HttpClient();
            // http//DockerJiraServer:8080/rest/api/2/issue/PL-122
            client.BaseAddress = new Uri("http://DockerJiraServer:8080/");
            client.DefaultRequestHeaders.Add("Authorization", "Basic Z3JvZHJpZ3Vlejpyb2RyaWd1ZXo=");
  
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            var apiQuery =
                string.Format(
                    "rest/api/2/issue/{0}",
                    key);
            HttpResponseMessage response = client.GetAsync(apiQuery).Result;
            
            if (response.IsSuccessStatusCode)
            {
                 returnObject = response.Content.ReadAsAsync<IssueDTO>().Result;         
            }
            else
            {
                returnObject = new IssueDTO { Error = "Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase };
            }
            return returnObject;
        }


        public IssuesDTO GetIssues(string filter, string jiraUri, string authorization)
        {
            IssuesDTO returnObject;
            var client = new HttpClient();

            client.BaseAddress = new Uri(jiraUri);

          //  client.DefaultRequestHeaders.Add("Authorization", "Basic Z3JvZHJpZ3Vlejpyb2RyaWd1ZXo=");
            client.DefaultRequestHeaders.Add("Authorization", authorization.Trim());

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            // API Query

            HttpResponseMessage asynResponse = client.GetAsync(filter.Trim()).Result;
         
            if (asynResponse.IsSuccessStatusCode)
            {
                returnObject = asynResponse.Content.ReadAsAsync<IssuesDTO>().Result;
            }
            else
            {
                returnObject = new IssuesDTO { Error = "Error Code" + asynResponse.StatusCode + " : Message - " + asynResponse.ReasonPhrase };
            }
            return returnObject;
        }
    }


    // Issue List
    public class IssuesDTO
    {
        public string Error { get; set; }
        public string Expand { get; set; }
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
        public List<IssueDTO> Issues { get; set; }

    } 
    



    // Main Issue
    public class IssueDTO : IssueBaseDTO
    {
        public string Error { get; set; }
        public IssueFields Fields { get; set; }
    }

    public class IssueFields : IssueBaseFields
    {
        public string Description { get; set; }
        public List<IssueSubTaskDTO> SubTasks { get; set; }
        public JiraUserInfo Assignee { get; set; }
        
    }

    // Sub-Task Issue
    public class IssueSubTaskDTO : IssueBaseDTO
    {
        public IssueBaseFields Fields { get; set; }
    }

    
    // Base Clases
    public class IssueBaseDTO
    {
        public string Key { get; set; }
        public string Self { get; set; }
    }

    public class IssueBaseFields
    {
        public string Summary { get; set; }
        public IssueStatus Status { get; set; }
        public IssueType IssueType { get; set; }
    }

    
    public class IssueType
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class IssueStatus
    {
        public string Name { get; set; }
        public bool SubTask { get; set; }
    }

    public class JiraUserInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        //public Avatar AvatarUrls {get; set; }
    }

    //public class Avatar
    //{
    //    public string 16x16 { get; set; }
    //    public string 24x24 { get; set; }
    //    public string 32x32 { get; set; }
    //    public string 48x48 { get; set; }

    //}
}
