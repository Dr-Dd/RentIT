using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services.Request;
using Newtonsoft.Json;
using RentIT.Models.User;


namespace RentIT.Services.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IRequestService requestService;

        //definire i valori ritornati per ogni metodo e le varie eccezioni

        public AuthenticationService(IRequestService requestService)
        {
            this.requestService = requestService;

        }

        public bool IsUserAuthenticated => !string.IsNullOrEmpty(AppSettings.AccessToken);  // determina see l'utente è attualmente loggato


        //metodo per effettuare il login 
        //prende in imput email e password e ritorna l'utente corrsipondente nel db , compresi dati e token che vengono storati localmente 
        public async Task<AuthenticationResponse> LoginAsync(string _email, string _password)
        {
            var builder = new UriBuilder(Constants.AuthEndpointLogin());
            string uri = builder.ToString();

            var auth = new AuthenticationRequest
            {
                email = _email,
                password = _password,
            };

            AuthenticationResponse authenticationInfo = await  requestService.PostAsync<AuthenticationRequest,AuthenticationResponse>(uri, auth);
            if (authenticationInfo.hasSucceded)
            {
                AppSettings.UserId = authenticationInfo.userId;
                AppSettings.AccessToken = authenticationInfo.accessToken;
                AppSettings.NewProfile = authenticationInfo.isFirstAccess;
            }
            return authenticationInfo;
        }


        //metodo per effettuare il logout , viene cancellato l'utente salvato localmente 
        public async Task<bool> LogoutAsync()
        {
            //gestisci logout eliminando nel db la entry corrispondente all'id inviato ,dalla tabella id-token

            
            var builder = new UriBuilder(Constants.AuthEndpointLogout());
            string uri = builder.ToString();

            AuthenticationResponse logOutInfo = await requestService.DeleteAsync<AuthenticationResponse>(uri, AppSettings.AccessToken);

            if (logOutInfo.hasSucceded == true)
            {
                AppSettings.RemoveAccessToken();
                AppSettings.RemoveUserId();
            }
                


            return logOutInfo.hasSucceded;
        }

        public long GetCurrentUserId()
        {
            return AppSettings.UserId;
        }



    }
}
