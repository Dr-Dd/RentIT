using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services.Request;
using Newtonsoft.Json;

namespace RentIT.Services.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        readonly IRequestService requestService;

        //definire i valori ritornati per ogni metodo e le varie eccezioni

        public AuthenticationService(IRequestService requestService)
        {
            this.requestService = requestService;

        }

        public bool IsUserAuthenticated => AppSettings.User != null;  // determina see l'utente è attualmente loggato

        public Models.Utente AuthenticatedUser => AppSettings.User; // prende l'utente corrente

        //meotodo per iscrivere un utente che prende i dati e li invia com json al db 
        //da definire cosa ritorna e le eccezioni 

        public Task<Models.Utente> IscriviUtenteAsync(Utente user)
        {
            // RestUrl = http://5.249.151.26:5000/Auth
            var builder = new UriBuilder(Constants.AuthEndpointIscrizione());
            var uri = builder.ToString();

            string token="";

            return requestService.PostAsync<Models.Utente>(uri, user, token);
         }
        

        //metodo per effettuare il login 
        //prende in imput email e password e ritorna l'utente corrsipondente nel db , compresi dati e token che vengono storati localmente 
        public Task<Models.Utente> LoginAsync(string email, string password)
        {
            // RestUrl = http://5.249.151.26:5000/Auth
            var builder = new UriBuilder(Constants.AuthEndpointLogin());
            string uri = builder.ToString();

            Utente user = new Utente
            {
                email = email,
                password = password
            };
            string token = "";

            return requestService.PostAsync<Models.Utente>(uri, user, token);
            
        }


        //metodo per effettuare il logout , viene cancellato l'utente salvato localmente e invia una richiesta di logout al db
        public async Task<bool> LogoutAsync()
        {
            //gestisci logout 

            AppSettings.RemoveUserData();
            return await Task.FromResult(true);
        }

        
    }
}
