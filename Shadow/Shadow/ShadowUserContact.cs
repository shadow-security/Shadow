using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Shadow
{
    public class ShadowUserContact
    {
        string firstname;
        string lastname;
        string phoneno;

        public string Id { get; set; }

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

    }
}
