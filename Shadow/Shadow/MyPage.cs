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
            ShadowService.OnSavedUserLoaded += OnLogin;

            var Username = new Entry { Placeholder = "Username" };
            var Password = new Entry { Placeholder = "Password" };

            Button btnResetPassword = new Button
            {
                Text = "Reset password"
            };

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

            Button btnLoginLogOut = new Button
            {
                Text = "Log Out"
            };

            btnLogin.Clicked += (sender, e) =>
            {
                Login(Username.Text, Password.Text);
            };

            btnResetPassword.Clicked += (sender, e) =>
            {
                ResetPassword(Username.Text);
            };

            btnLoginLogOut.Clicked += (sender, e) =>
            {
                ShadowService.Logout();
            };


            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" },
                    Username,
                    Password,
                    btnReg,
                    btnLogin,
                    btnResetPassword,
                    btnLoginLogOut
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
                    account.firstName = "Marius";
                    Contact contact = new Contact();
                    contact.firstName = "Jan";
                    contact.lastName = "Botha";
                    contact.phoneNo = "0828213175";
                    ShadowService.CurrentUser.addEmergencyContact(contact);
                    await ShadowService.Addlog(0, "added contact", "user contact");
                    await ShadowService.SaveCurrentUser();
                    //await ShadowService.sendSMS("0828213175", "TEST");
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

        private async void LoginTwitter()
        {
            try
            {
                Account account = await ShadowService.AuthenticateTwitter();
                if (account != null)
                {
                    ShadowService.CurrentUser.lastName = "Bloemhof2";
                    ShadowService.CurrentUser.Lat = 41.2564654;
                    //Contact contact = new Contact();
                    //contact.firstName = "Jan";
                    //contact.lastName = "Botha";
                    //contact.phoneNo = "0828213175";
                    //ShadowService.CurrentUser.addEmergencyContact(contact);
                    //ShadowService.Addlog(0, "added contact", "user contact");
                    //ShadowService.SaveCurrentUser();
                    await DisplayAlert("Success", "Logged in with Twitter", "OK");
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
        private async void ResetPassword(String username)
        {
            try
            {
                if (ShadowService.CurrentUser.socialid == null)
                {
                    var response = await ShadowService.ResetPassword(username);
                    await DisplayAlert("Success", response, "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Social login, no need to reset", "OK");
                }

            }
            catch (MobileServiceInvalidOperationException ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnLogin(object sender, EventArgs e)
        {
            DisplayAlert("Account retrieved", ShadowService.CurrentUser.Id, "OK");
        }
    }
}


