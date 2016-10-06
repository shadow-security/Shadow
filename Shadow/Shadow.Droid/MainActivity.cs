using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Shadow;
using System.Threading.Tasks;
using Shadow.Model;

namespace Shadow.Droid
{
    [Activity(Label = "Shadow", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var App = new Shadow.App();
            LoadApplication(App);
           
            
        }

        public void loginHandler(object sender, EventArgs args)
        {

            ShadowService.CurrentUser.lastName = "Bloemhof";
            Contact contact = new Contact();
            contact.firstName = "Jan";
            contact.lastName = "Botha";
            contact.phoneNo = "0828213175";
            ShadowService.CurrentUser.addEmergencyContact(contact);
            ShadowService.Addlog(0, "added contact", "user contact");
            ShadowService.SaveCurrentUser().Wait();

            ShadowService.sendSMS("+27828213175", "Login successfull").Wait();

            //ShadowService.RegisterAccount("marius@bloemhofs.co.za", "MyPassword123");
            //ShadowService.sendSMS("+27828213175", "Hello from SHADOW").Wait();

        }

        public void loginFailed(object sender, ErrorEventArgs args)
        {
            try
            {
                ShadowService.sendSMS("+27828213175", "Login failed: " + args.Result.ToString()).Wait();
            }
            catch (Exception ex)
            {
            }
        }
    }
}

