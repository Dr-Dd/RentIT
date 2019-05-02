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
        public async Task<AuthenticationResponse> LoginAsync(string email, string password)
        {
            // RestUrl = http://5.249.151.26:5000/Auth
            var builder = new UriBuilder(Constants.AuthEndpointLogin());
            string uri = builder.ToString();

            var auth = new AuthenticationRequest
            {
                Email = email,
                Password = password,
            };
            string token = "";

            AuthenticationResponse authenticationInfo = await  requestService.PostAsync<AuthenticationRequest,AuthenticationResponse>(uri, auth , token);
            if (authenticationInfo.HasSucceded == true)
            {
                AppSettings.UserId = authenticationInfo.UserId;
                AppSettings.AccessToken = authenticationInfo.AccessToken;
            }
            return authenticationInfo;
        }


        //metodo per effettuare il logout , viene cancellato l'utente salvato localmente 
        public async Task<bool> LogoutAsync()
        {
            //gestisci logout 

            AppSettings.RemoveUserId();
           
            AppSettings.RemoveAccessToken();
            

            return await Task.FromResult(true);
        }

        
    }
}
