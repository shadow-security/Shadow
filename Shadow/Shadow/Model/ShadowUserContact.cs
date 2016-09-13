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
        bool _changed = false;

        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId
        {
            get { return userid; }
            set { userid = value; _changed = true; }
        }

        [JsonProperty(PropertyName = "firstname")]
        public string firstName
        {
            get { return firstname; }
            set { firstname = value; _changed = true;  }
        }

        [JsonProperty(PropertyName = "lastname")]
        public string lastName
        {
            get { return lastname; }
            set { lastname = value; _changed = true; }
        }

        [JsonProperty(PropertyName = "phoneno")]
        public string phoneNo
        {
            get { return phoneno; }
            set { phoneno = value; _changed = true; }
        }

        public bool deleted
        {
            get { return deleted_; }
            set { deleted_ = value; }
        }

        public bool Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }
    }
}
