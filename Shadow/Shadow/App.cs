using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace Shadow
{
	public class App : Application
	{
		public App ()
		{
            /* Usage */
            //btnGoogle.Clicked += async (sender, args) =>
            //{
            //    var user = await Login.AuthenticateGoogle();
            //    welcomeLabel.Text = string.Format("Welcome {0}!", user.Message.Email);
            //};

           
            
            MainPage = new ContentPage
            {

            };
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
