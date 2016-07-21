using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shadow
{
    public class Audit
    {
        string userid;
        int eventstatus;
        string eventdescription;
        string eventtype;
        DateTime timestamp;
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId
        {
            get { return userid; }
            set { userid = value; }
        }

        [JsonProperty(PropertyName = "eventDescription")]
        public string eventDescription
        {
            get { return eventdescription; }
            set { eventdescription = value; }
        }

        [JsonProperty(PropertyName = "eventType")]
        public string eventType
        {
            get { return eventtype; }
            set { eventtype = value; }
        }

        [JsonProperty(PropertyName = "timeStamp")]
        public DateTime timeStamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        [JsonProperty(PropertyName = "eventStatus")]
        public int eventStatus
        {
            get { return eventstatus; }
            set { eventstatus = value; }
        }

    }
}
