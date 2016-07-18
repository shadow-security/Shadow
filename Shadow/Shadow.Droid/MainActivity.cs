using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Shadow;
using System.Threading.Tasks;

namespace Shadow.Droid
{
    [Activity(Label = "Shadow", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new Shadow.App());

            ShadowService.AuthenticateFacebook();
            ShadowService.onAuthenticated += loginHandler;

            //var task = Task.Run(async () => { await ShadowService.AuthenticateFacebook(); });
            //task.Wait();

        }

        public void loginHandler(object sender, EventArgs args)
        {
            ShadowService.CurrentUser.lastName = "Bloemhof";
            ShadowUserContact contact = new ShadowUserContact();
            contact.firstName = "Jan";
            contact.lastName = "Botha";
            contact.phoneNo = "0828213175";
            ShadowService.CurrentUser.addEmergencyContact(contact);
            ShadowService.SaveCurrentUser();
        }

    }
}

