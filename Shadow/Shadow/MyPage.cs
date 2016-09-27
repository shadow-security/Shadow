using Microsoft.WindowsAzure.MobileServices;
using Shadow.Model;
using System;

using Xamarin.Forms;

namespace Shadow
{
	public class MyPage : ContentPage
	{
		public MyPage()
		{
            var Username = new Entry { Placeholder = "Username" };
            var Password = new Entry { Placeholder = "Username" };

            Button btnReg = new Button
			{
				Text = "Register"
			};

            btnReg.Clicked += (sender, e) =>
            {
                Register(Username.Text, Password.Text);
            };

            Button btnLogin = new Button
            {
                Text = "Login"
            };

            Button btnLoginFaceBook= new Button
            {
                Text = "Login with FaceBook"
            };

            btnLogin.Clicked += (sender, e) =>
			{
                Login(Username.Text, Password.Text);
			};

            btnLoginFaceBook.Clicked += (sender, e) =>
            {
                LoginFaceBook();
            };

            Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" },
                    Username,
                    Password,
                    btnReg,
                    btnLogin,
                    btnLoginFaceBook
                }
			};


		}

		private async void Register(String username, String password)
		{
            try
            {
                Account account = await ShadowService.RegisterAccount(username, password);
                if (account != null)
                {
                    ShadowService.CurrentUser.lastName = "Bloemhof2";
                    //Contact contact = new Contact();
                    //contact.firstName = "Jan";
                    //contact.lastName = "Botha";
                    //contact.phoneNo = "0828213175";
                    //ShadowService.CurrentUser.addEmergencyContact(contact);
                    await ShadowService.Addlog(0, "added contact", "user contact");
                    //await ShadowService.SaveCurrentUser();
                    await DisplayAlert("Success", "Registered new account: "+ username, "OK");
                }

            }
            catch (MobileServiceInvalidOperationException ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void Login(String username, String password)
        {
            try
            {
                Account account = await ShadowService.AuthenticateUser(username, password);
                if (account != null)
                {
                    //ShadowService.CurrentUser.lastName = "Bloemhof2";
                    //Contact contact = new Contact();
                    //contact.firstName = "Jan";
                    //contact.lastName = "Botha";
                    //contact.phoneNo = "0828213175";
                    //ShadowService.CurrentUser.addEmergencyContact(contact);
                    //await ShadowService.Addlog(0, "added contact", "user contact");
                    //await ShadowService.SaveCurrentUser();
                    await DisplayAlert("Success", "Logged in account: " + username, "OK");
                }

            }
            catch (MobileServiceInvalidOperationException ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void LoginFaceBook()
        {
            try
            {
                Account account = await ShadowService.AuthenticateFacebook();
                if (account != null)
                {
                    ShadowService.CurrentUser.lastName = "Bloemhof2";
                    //Contact contact = new Contact();
                    //contact.firstName = "Jan";
                    //contact.lastName = "Botha";
                    //contact.phoneNo = "0828213175";
                    //ShadowService.CurrentUser.addEmergencyContact(contact);
                    //ShadowService.Addlog(0, "added contact", "user contact");
                    //ShadowService.SaveCurrentUser();
                    await DisplayAlert("Success", "Logged in with Facebook", "OK");
                }

            }
            catch (MobileServiceInvalidOperationException ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}


