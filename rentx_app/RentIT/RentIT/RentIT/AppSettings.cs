using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace RentIT
{
    public class AppSettings
    {
        private static ISettings Settings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string UserIdKey = "user_id_key";
        private static readonly int UserIdDefault = 0;

        private const string AccessTokenKey = "access_token_key";
        private static readonly string AccessTokenDefault = string.Empty;

        private const string NewProfilekey = "new_profile_key";
        private static readonly bool NewProfileDefault = true;



        public static long UserId
        {
            get
            {
                return Settings.GetValueOrDefault(UserIdKey, UserIdDefault);
            }
            set
            {
                Settings.AddOrUpdateValue(UserIdKey, value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return Settings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault);
            }
            set
            {
                Settings.AddOrUpdateValue(AccessTokenKey, value);
            }
        }

        public static bool NewProfile
        {
            get
            {
                return Settings.GetValueOrDefault(NewProfilekey, NewProfileDefault);
            }
            set
            {
                Settings.AddOrUpdateValue(NewProfilekey, value);
            }
        }

        public static void RemoveUserId()
        {
            Settings.Remove(UserIdKey);
        }

        public static void RemoveAccessToken()
        {
            Settings.Remove(AccessTokenKey);
        }

        public static void RemoveNewProfile()
        {
            Settings.Remove(NewProfilekey);
        }

        public static void RemoveAll()
        {
            RemoveAccessToken();
            RemoveNewProfile();
            RemoveUserId();
        }

    }
}
