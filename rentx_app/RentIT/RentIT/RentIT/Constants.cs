using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT
{
    public static class Constants
    {
        /* 
         * Ricordate di cambiare l'indirizzo IPv4 degli endpoint in maniera tale da 
         * averlo uguale a quello del server REST su spring! Per altre info andate a
         * vedere il progetto della backend, in particolare 'application.properties -> server.address'
         * 
         * Ad esempio, visto che li l'ip sul mio pc è 192.168.1.83, allora quello degli endpoint dovrà 
         * essere uguale! (Come in questo caso)
         */
        
        // URL of REST service
        public static string RestUrl = "http://192.168.1.226:8080/{0}";
        // defaul Endpoints for api
        const string defaultAuthenticationEndpoint = "http://192.168.1.226:8080/auth/{0}";
        const string defaultItemEndpoint = "http://192.168.1.226:8080/item/{0}";
        const string defaultProductEndpoint = "http://192.168.1.226:8080/product/{0}";
        //utente minuscolo
        const string defaultUserEndpoint = "http://192.168.1.226:8080/utente/{0}";
        



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
            //qui anche registrazione invece di iscrizione
            return string.Format(Constants.defaultUserEndpoint, "registrazione");
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
