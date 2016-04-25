using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;

namespace Skunkworks.Ics.Web.Managers
{
    public class SlackManager
    {
        private readonly Uri _uri;
        //private readonly Encoding _encoding = new UTF8Encoding();

        public SlackManager()
        {
            _uri = new Uri("https://hooks.slack.com/services/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");
        }

        //Post a message using simple strings  
        public void PostMessage(string text, string username = "Mr. Log Manager", string channel = "#logs")
        {
            var payload = new Payload()
            {
                Channel = channel,
                Username = username,
                Text = text
            };

            PostMessage(payload);
        }

        //Post a message using a Payload object  
        public void PostMessage(Payload payload)
        {
            var payloadJson = JsonConvert.SerializeObject(payload);

            using (var client = new WebClient())
            {
                var data = new NameValueCollection();
                data["payload"] = payloadJson;

                client.UploadValues(_uri, "POST", data);
                //var response = client.UploadValues(_uri, "POST", data);
                //The response text is usually "ok"  
                //string responseText = _encoding.GetString(response);
            }
        }
    }

    //This class serializes into the Json payload required by Slack Incoming WebHooks  
    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}