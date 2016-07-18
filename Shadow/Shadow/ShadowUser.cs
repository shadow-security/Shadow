using Newtonsoft.Json;

namespace Shadow
{
    public class ShadowUser
    {
        string firstname;
        string lastname;
        string phoneno;
        string userid;
        string idno;
        string medicalprovider;
        string medicalproviderphoneno;
        string securityprovider;
        string securityproviderphoneno;


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

        [JsonProperty(PropertyName = "idno")]
        public string idNo
        {
            get { return idno; }
            set { idno = value; }
        }

        [JsonProperty(PropertyName = "medicalprovider")]
        public string medicalProvider
        {
            get { return medicalprovider; }
            set { medicalprovider = value; }
        }

        [JsonProperty(PropertyName = "medicalproviderphoneno")]
        public string medicalproviderPhoneno
        {
            get { return medicalproviderphoneno; }
            set { medicalproviderphoneno = value; }
        }

        [JsonProperty(PropertyName = "securityprovider")]
        public string securityProvider
        {
            get { return securityprovider; }
            set { securityprovider = value; }
        }

        [JsonProperty(PropertyName = "securityproviderphoneno")]
        public string securityproviderPhoneno
        {
            get { return securityproviderphoneno; }
            set { securityproviderphoneno = value; }
        }

    }
}
