using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Shadow
{
    public class ShadowUserContact
    {
        string userid;
        string firstname;
        string lastname;
        string phoneno;
        bool deleted_;

        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId
        {
            get { return userid; }
            set { userid = value; }
        }

        [JsonProperty(PropertyName = "firstname")]
        public string firstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        [JsonProperty(PropertyName = "lastname")]
        public string lastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        [JsonProperty(PropertyName = "phoneno")]
        public string phoneNo
        {
            get { return phoneno; }
            set { phoneno = value; }
        }

        public bool deleted
        {
            get { return deleted_; }
            set { deleted_ = value; }
        }
    }
}
