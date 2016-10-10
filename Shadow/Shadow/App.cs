using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Shadow.Model;

namespace Shadow
{
	public class App : Application
	{
		public App ()
		{
            MainPage = new MyPage();
            {

            };


        }

        protected async override void OnStart ()
		{
            // Handle when your app starts
            Account account = await ShadowService.GetLoggedinUser();
            
        }

		protected override void OnSleep ()
		{
            // Handle when your app sleeps
            ShadowService.SaveLoggedinUser();

        }

		protected async override void OnResume ()
		{
            // Handle when your app resumes
            
        }
	}
}
