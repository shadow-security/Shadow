using System;

using Xamarin.Forms;

namespace Shadow
{
	public class MyPage : ContentPage
	{
		public MyPage()
		{

			Button btnReg = new Button
			{
				Text = "Register"
			};

            btnReg.Clicked += (sender, e) =>
            {
                Register();
            };

            Button btnLogin = new Button
            {
                Text = "Login"
            };

            btnLogin.Clicked += (sender, e) =>
			{
                Login();
			};

			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" },
					btnReg,
                    btnLogin
                }
			};


		}

		private async void Register()
		{
			ShadowService.onAuthenticationFailed += (sender, e) =>
			{
				Console.WriteLine(e.Result);
			};
			ShadowUser user = await ShadowService.RegisterAccount("erhard1@me.net", "Password123");
			if (user != null)
			{
                ShadowUserContact contact = new ShadowUserContact();
                contact.firstName = "Jan";
                contact.lastName = "Botha";
                contact.phoneNo = "0828213175";
                ShadowService.CurrentUser.addEmergencyContact(contact);
                ShadowService.Addlog(0, "added contact", "user contact");
                ShadowService.SaveCurrentUser();
                Console.WriteLine("Ok");
			}
		}

        private async void Login()
        {
            ShadowService.onAuthenticationFailed += (sender, e) =>
            {
                Console.WriteLine(e.Result);
            };
            ShadowUser user = await ShadowService.AuthenticateUser("erhard1@me.net", "Password123");
            if (user != null)
            {
                ShadowService.CurrentUser.lastName = "Bloemhof";
                ShadowUserContact contact = new ShadowUserContact();
                contact.firstName = "Jan";
                contact.lastName = "Botha";
                contact.phoneNo = "0828213175";
                ShadowService.CurrentUser.addEmergencyContact(contact);
                ShadowService.Addlog(0, "added contact", "user contact");
                ShadowService.SaveCurrentUser();
                Console.WriteLine("Ok");
            }
        }
    }
}


