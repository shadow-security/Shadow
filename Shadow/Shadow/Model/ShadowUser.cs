using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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
        List<ShadowUserContact> emergencycontacts = new List<ShadowUserContact>();

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

        public List<ShadowUserContact> EmergencyContacts
        {
            get { return emergencycontacts; }
        }

        public void addEmergencyContact(ShadowUserContact emergencyContact)
        {
            emergencyContact.UserId = UserId;
            emergencycontacts.Add(emergencyContact);
        }

    }
}
