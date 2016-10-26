using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Android.Telephony;
using Android.Content;

namespace Shadow.Droid
{
	public class App : Application
	{
		public App ()
		{
            MainPage = new MyPage();          
		}

        

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
