using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shadow.Model
{
    public class Account
    {
        List<Contact> emergencycontacts = new List<Contact>();

        public string Id { get; set; }

        [JsonProperty(PropertyName = "SocialId")]
        public string socialid { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string firstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string lastName { get; set; }

        [JsonProperty(PropertyName = "PhoneNo")]
        public string phoneNo { get; set; }

        [JsonProperty(PropertyName = "IdNo")]
        public string idNo { get; set; }

        [JsonProperty(PropertyName = "MedicalProvider")]
        public string medicalProvider { get; set; }

        [JsonProperty(PropertyName = "MedicalProviderPhoneNo")]
        public string medicalproviderPhoneno { get; set; }

        [JsonProperty(PropertyName = "SecurityProvider")]
        public string securityProvider { get; set; }

        [JsonProperty(PropertyName = "SecurityProviderPhoneNo")]
        public string securityproviderPhoneno { get; set; }

        [JsonProperty(PropertyName = "EmailAddress")]
        public string email { get; set; }

        [JsonProperty(PropertyName = "Salt")]
        public string salt { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string status { get; set; }

        public List<Contact> ContactList
        {
            get { return emergencycontacts; }
        }

        public void addEmergencyContact(Contact emergencyContact)
        {
            emergencyContact.userId = Id;
            emergencycontacts.Add(emergencyContact);
        }

    }
}
