using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT
{
    public static class Constants
    {
        // URL of REST service
        public static string RestUrl = "http://192.168.1.173:8080/{0}";
        // defaul Endpoints for api
        const string defaultAuthenticationEndpoint = "http://192.168.1.173:8080/auth/{0}";
        const string defaultItemEndpoint = "http://192.168.1.173:8080/item/{0}";
        const string defaultProductEndpoint = "http://192.168.1.173:8080/product/{0}";
        const string defaultUserEndpoint = "http://192.168.1.173:8080/utente/{0}";



          public static string AuthEndpointLogin()
        {
            return string.Format(Constants.RestUrl, "login");
        }

        public static string AuthEndpointLogout()
        {
            return string.Format(Constants.defaultAuthenticationEndpoint, "logout");
        }

        public static string UserEndpointIscrizione()
        {
            
            return string.Format(Constants.defaultUserEndpoint, "registrazione");
        }

        public static string UserEndpontEliminaAccount()
        {
            return string.Format(Constants.defaultUserEndpoint, "elimina");
        }

        public static string UserEndpointModifyData()
        {
            return string.Format(Constants.defaultUserEndpoint, "modifica");
        }

        public static string UserEndpointCurrentData()
        {
            return string.Format(Constants.defaultUserEndpoint, "profile");
        }

        public static string UserEndpointUpImage()
        {
            return string.Format(Constants.defaultUserEndpoint, "addImage");
        }

        public static string UserEndpointGetImage()
        {
            return string.Format(Constants.defaultUserEndpoint, "image");
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
