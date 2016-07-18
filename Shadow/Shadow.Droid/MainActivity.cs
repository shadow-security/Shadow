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

            //var task = Task.Run(async () => { await ShadowService.AuthenticateFacebook(); });
            //task.Wait();

            //ShadowService.CurrentUser().lastName = "Bloemhof";
            //ShadowService.SaveCurrentUser();
        }
    }
}

