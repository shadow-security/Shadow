//#define OFFLINE_SYNC_ENABLED

using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Twilio;
using Twilio.Clients;
using Twilio.Creators.Api.V2010.Account;
using Twilio.Types;

namespace Shadow
{
    public static class ShadowService
    {
        private static ShadowUser user;
        private static Boolean isAuthenticated;
        private static readonly MobileServiceClient Client = new MobileServiceClient(Constants.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
        private static IMobileServiceSyncTable<ShadowUser> ShadowUserTable;
        private static IMobileServiceSyncTable<ShadowUserContact> ShadowUserContactTable;
        private static IMobileServiceSyncTable<Audit> ShadowAuditTable;
#else
        private static IMobileServiceTable<ShadowUser> ShadowUserTable;
        private static IMobileServiceTable<ShadowUserContact> ShadowUserContactTable;
        private static IMobileServiceTable<Audit> ShadowAuditTable;
#endif

        static ShadowService()
        {
#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore("localstore.db");
            store.DefineTable<ShadowUser>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            Client.SyncContext.InitializeAsync(store);

            ShadowUserTable = Client.GetSyncTable<ShadowUser>();
            ShadowUserContactTable = Client.GetSyncTable<ShadowUserContact>();
            ShadowAuditTable = Client.GetSyncTable<Audit>();
#else
            ShadowUserTable = Client.GetTable<ShadowUser>();
            ShadowUserContactTable = Client.GetTable<ShadowUserContact>();
            ShadowAuditTable = Client.GetTable<Audit>();
#endif

        }

        private static async Task<ShadowUser> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            try
            {
                MobileServiceUser authUser;
                isAuthenticated = false;
#if __IOS__
                authUser = await Client.LoginAsync(UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController, provider);
#elif WINDOWS_PHONE
                authUser = return await Client.LoginAsync(provider);
#else
                authUser = await Client.LoginAsync(
                    Xamarin.Forms.Forms.Context, 
                    provider);
#endif
                isAuthenticated = (authUser.UserId != String.Empty);
                //if user is authenticated, fetch the corresponding user object
                IMobileServiceTableQuery<ShadowUser> userquery = ShadowUserTable.Where(t => t.UserId == authUser.UserId);
                var userres = await userquery.ToListAsync();
                if (userres.Count > 0)
                {
                    user = userres.Find(t => t.UserId == authUser.UserId);

                    IMobileServiceTableQuery<ShadowUserContact> contactquery = ShadowUserContactTable.Where(t => t.UserId == user.UserId);
                    var contactsres = await contactquery.ToListAsync();
                    foreach (ShadowUserContact contact in contactsres)
                    {
                        user.addEmergencyContact(contact);
                    }

                }
                //if none exists, create a new object
                else
                {
                    user = new ShadowUser();
                    user.UserId = authUser.UserId;
                    await SaveTaskAsync(user);
                }
                RaiseOnAuthenticated();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static async Task SaveTaskAsync(ShadowUser item)
        {
            if (item.Id == null)
            {
                await ShadowUserTable.InsertAsync(item);
            }
            else
            {
                await ShadowUserTable.UpdateAsync(item);
            }
        }

        public static async Task<ShadowUser> AuthenticateGoogle()
        {
            return await Authenticate(MobileServiceAuthenticationProvider.Google);
        }

        public static async Task<ShadowUser> AuthenticateTwitter()
        {
            return await Authenticate(MobileServiceAuthenticationProvider.Twitter);
        }

        public static async Task<ShadowUser> AuthenticateFacebook()
        {
            return await Authenticate(MobileServiceAuthenticationProvider.Facebook);
        }

        public static ShadowUser CurrentUser
        {
           get
            {
                if (isAuthenticated)
                {
                    return user;
                }
                else
                {
                    throw new System.InvalidOperationException("User not logged in");
                }    
                    
            }
        }

        public static async Task SaveCurrentUser()
        {
            if (CurrentUser.Id == null)
            {
                await ShadowUserTable.InsertAsync(CurrentUser);
            }
            else
            {
                foreach (ShadowUserContact contact in CurrentUser.EmergencyContacts)
                {
                    if (contact.Id == null)
                    {
                        await ShadowUserContactTable.InsertAsync(contact);
                    }
                    else
                    {
                        if (contact.deleted)
                        { 
                            await ShadowUserContactTable.DeleteAsync(contact);
                        }
                        else {
                            await ShadowUserContactTable.UpdateAsync(contact);
                        }
                        
                    }
                }
                await ShadowUserTable.UpdateAsync(CurrentUser);
            }

        }
        
        public static async Task<Boolean> sendSMS(string phoneno, string message)
        {
            Boolean Delivered = false;
            try
            {
                Delivered = await SendSms(phoneno, message);
                if (Delivered)
                {
                    await ShadowService.Addlog(0, "Delivery to ["+phoneno+"] succeeded", "SMS");
                    RaiseOnSMSDelivered(phoneno);
                    return true;
                }
                else
                {
                    await ShadowService.Addlog(-1, "Delivery to [" + phoneno + "] failed", "SMS");
                    RaiseonSMSFailed(phoneno);
                    return false;
                }
            }
            catch (Exception ex)
            {
                await ShadowService.Addlog(-2, "Delivery to [" + phoneno + "] failed", "SMS");
                RaiseonSMSFailed(phoneno);
                return false;
            }



        }

        public static async Task Addlog(int eventstatus, string eventdescription, string eventtype)
        {
            Audit logentry = new Audit();

            logentry.eventStatus = eventstatus;
            logentry.eventDescription = eventdescription;
            logentry.eventType = eventtype;
            logentry.timeStamp = DateTime.Now.ToUniversalTime();

            await ShadowAuditTable.InsertAsync(logentry);
        }

#if OFFLINE_SYNC_ENABLED
        public static async Task SyncData()
        {
            await Client.SyncContext.PushAsync();
        }
#endif        

        /*Event handlers*/
        public static event EventHandler onAuthenticated;

        public static event EventHandler onSMSDelivered;

        public static event EventHandler onSMSFailed;

        private static void RaiseOnAuthenticated()
        {
            var handler = onAuthenticated;
            if (handler != null)
                handler(typeof(ShadowService), EventArgs.Empty);
        }

        private static void RaiseOnSMSDelivered(string phoneno)
        {
            var handler = onSMSDelivered;
            if (handler != null)
                handler(typeof(ShadowService), EventArgs.Empty);
        }

        private static void RaiseonSMSFailed(string phoneno)
        {
            var handler = onSMSFailed;
            if (handler != null)
                handler(typeof(ShadowService), EventArgs.Empty);
        }


        public static async Task<Boolean> SendSms(string phoneno, string smsMessage)
        {
            //method 1 - RouteSMS
            return RouteSMS.SendSMS(phoneno, smsMessage);

            //method 2 - Twilio
            //var accountSid = "ACffd7aeddc478c222a68b1ac151662c7b"; // Your Account SID from twilio.com/console
            //var authToken = "3c3c7dbf39ec3b28a116d06f69d4aa60";   // Your Auth Token from twilio.com/console

            //TwilioClient.Init(accountSid, authToken);
            //var twilio = new TwilioRestClient(accountSid, authToken);
            //PhoneNumber toNum = new PhoneNumber(phoneno);
            //    //"++27828213175"

            //var message = await
            //  new MessageCreator(
            //    accountSid,
            //    toNum,  // To number
            //    new PhoneNumber("+14129619401"),  // Twilio From number +14129619401
            //    smsMessage
            //  ).ExecuteAsync(twilio);
            // Console.WriteLine("Message sent");
            //return true;
        }


    }
}