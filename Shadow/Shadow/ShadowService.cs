using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
//#define OFFLINE_SYNC_ENABLED


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
#else
        private static IMobileServiceTable<ShadowUser> ShadowUserTable;
        private static IMobileServiceTable<ShadowUserContact> ShadowUserContactTable;
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
#else
            ShadowUserTable = Client.GetTable<ShadowUser>();
            ShadowUserContactTable = Client.GetTable<ShadowUserContact>();
#endif

        }

        private static async Task<ShadowUser> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            try
            {
                MobileServiceUser authUser;
                isAuthenticated = false;
#if __IOS__
                authUser = return await Client.LoginAsync(UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController, provider);
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

                    IMobileServiceTableQuery<ShadowUserContact> contactquery = ShadowUserContactTable.Where(t => t.Id == user.Id);
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
                RaiseonAuthenticated();
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
                        await ShadowUserContactTable.UpdateAsync(contact);
                    }
                }
                await ShadowUserTable.UpdateAsync(CurrentUser);
            }

        }


        public static event EventHandler onAuthenticated;

        private static void RaiseonAuthenticated()
        {
            var handler = onAuthenticated;
            if (handler != null)
                handler(typeof(ShadowService), EventArgs.Empty);
        }

        public static async Task sendSMS(string message)
        {
            //send SMS
        }



    }
}