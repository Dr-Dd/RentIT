using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Models.Image;
using RentIT.Models.User;
using RentIT.Services.Authentication;
using RentIT.Services.Request;

namespace RentIT.Services.User
{
    public class UserService : IUserService
    {

        private readonly IRequestService requestService;
        

        public UserService(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        public Task<Response> SignUpAsync(SignUpRequest request)
        {
            // RestUrl = http://5.249.151.26:5000/
            var builder = new UriBuilder(Constants.UserEndpointIscrizione());
            var uri = builder.ToString();

            return requestService.PostAsync<SignUpRequest,Response>(uri, request);
        }

        public Task<Utente> GetCurrentProfileAsync()
        {

            var builder = new UriBuilder(Constants.UserEndpointCurrentData());
            var uri = builder.ToString();

            return requestService.GetAsync<Utente>(uri,AppSettings.AccessToken);
        }


        public async Task<Response> ModifyUserData(Utente u)
        {
            var builder = new UriBuilder(Constants.UserEndpointModifyData());
            var uri = builder.ToString();

            Response resp = await requestService.PutAsync<Utente,Response>(uri,u, AppSettings.AccessToken);

            return resp;
        }

        public async Task<Response> DeleteAccount()
        {
            var builder = new UriBuilder(Constants.UserEndpontEliminaAccount());
            var uri = builder.ToString();

            Response resp = await requestService.DeleteAsync<Response>(uri, AppSettings.AccessToken);

            if (resp.hasSucceded == true)
            {
                AppSettings.RemoveAccessToken();
                AppSettings.RemoveUserId();
            }

            return resp;
        }
    }
}
