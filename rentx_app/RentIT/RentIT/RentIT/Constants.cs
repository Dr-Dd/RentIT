using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT
{
    public static class Constants
    {
        // URL of REST service
        public static string RestUrl = "http://5.249.151.26:5000/{0}";
        // defual Endpoints for api
        const string defaultAuthenticationEndpoint = "http://5.249.151.26:5000/Auth/{0}";
        const string defaultItemEndpoint = "http://5.249.151.26:5000/Item/{0}";
        const string defaultProductEndpoint = "http://5.249.151.26:5000/Product/{0}";
        const string defaultUserEndpoint = "http://5.249.151.26:5000/User/{0}";



        public static string AuthEndpointIscrizione()
        {
            return string.Format(Constants.defaultAuthenticationEndpoint, "Iscrizione");
        }

        public static string AuthEndpointLogin()
        {
            return string.Format(Constants.defaultAuthenticationEndpoint, "Login");
        }

        public static string AuthEndpointLogout()
        {
            return string.Format(Constants.defaultAuthenticationEndpoint, "Logout");
        }

        public static string AuthEndpointModifyData()
        {
            return string.Format(Constants.defaultAuthenticationEndpoint, "Modifica");
        }

        public static string ItemEndpoint()
        {
            return defaultItemEndpoint;
        }

        public static string ProductEndpoint()
        {
            return defaultProductEndpoint;
        }
    }
}
