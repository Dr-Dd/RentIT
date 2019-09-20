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
        public static string RestUrl = "http://192.168.178.48:8080/{0}";

        // defaul Endpoints for api
        const string defaultAuthenticationEndpoint = "http://192.168.178.48:8080/auth/{0}";
        const string defaultItemEndpoint = "http://192.168.178.48:8080/item/{0}";
        const string defaultAnnuncioEndpoint = "http://192.168.178.48:8080/annuncio/{0}";
        const string defaultUserEndpoint = "http://192.168.178.48:8080/utente/{0}";


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

        public static string UserEndpointProfileData()
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

        public static string newAnnuncioEndpoint()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "newAnnuncio");
        }

        public static string eliminaAnnuncioEndpoint()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "elimina");
        }

        public static string modificaAnnuncioEndpoint()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "modifica");
        }

        public static string uploadImgsAnnuncioEndpoint()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "addImage");
        }

        public static string getImgsAnnuncioEndpoint()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "images");
        }


        public static string AnnunciPerUserEndpoint()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "annunci");
        }

        public static string AnnuncioPerId()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "annuncio");
        }

        public static string UltimiAnnunciEndpoint()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "search");
        }

        public static string PrenotaAnnuncio()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "prenota");
        }

        public static string LiberaAnnuncio()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "libera");
        }

        public static string isAdBooked()
        {
            return string.Format(Constants.defaultAnnuncioEndpoint, "isBooked");
        }


        public static string ItemEndpoint()
        {
            return defaultItemEndpoint;
        }

       
    }
}
