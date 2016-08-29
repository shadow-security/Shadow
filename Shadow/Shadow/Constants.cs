using System;

namespace Shadow
{
    public enum AccountResult
    {
        error,
        emailExists,
        passwordTooShort,
        accountCreated,
        loginSuccess,
        loginInvalidPassword,
        loginInvalidUser,
    }

    public static class Constants
    {
        // Replace strings with your mobile services and gateway URLs.
        public static string ApplicationURL = @"https://test-shadow-mobapp.azurewebsites.net";
        public static string RegisterURL = @"https://test-shadow-mobapp.azurewebsites.net/api/register";

    }
}

