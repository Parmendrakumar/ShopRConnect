using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace RetailerApp.AzureData
{
    public class TodoItem
    {
        string id;
        string message;
        bool done;
        string queryid;
        string rating;
        string customerid;
        string retailerid;
        string status;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "text")]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        [JsonProperty(PropertyName = "QueryID")]
        public string QueryID
        {
            get { return queryid; }
            set { queryid = value; }
        }

        [JsonProperty(PropertyName = "Rating")]
        public string Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        [JsonProperty(PropertyName = "CustomerID")]
        public string CustomerID
        {
            get { return customerid; }
            set { customerid = value; }
        }

        [JsonProperty(PropertyName = "RetailerID")]
        public string RetailerID
        {
            get { return retailerid; }
            set { retailerid = value; }
        }
        [JsonProperty(PropertyName = "Status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [JsonProperty(PropertyName = "complete")]
        public bool Done
        {
            get { return done; }
            set { done = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
