using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Models.User;
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

        public Task<SignUpResponse> SignUpAsync(SignUpRequest request)
        {
            // RestUrl = http://5.249.151.26:5000/
            var builder = new UriBuilder(Constants.UserEndpointIscrizione());
            var uri = builder.ToString();

            return requestService.PostAsync<SignUpRequest,SignUpResponse>(uri, request);
        }

        public Task<Utente> GetCurrentProfileAsync()
        {

            var builder = new UriBuilder(Constants.UserEndpointCurrentData());
            var uri = builder.ToString();

            return requestService.GetAsync<Utente>(uri,AppSettings.AccessToken);
        }

        public async Task UploadUserImageAsync(string imageAsBase64)
        {


            var builder = new UriBuilder(Constants.UserEndpointUpImage());
            builder.Path = "/" + AppSettings.UserId.ToString();
            var uri = builder.ToString();

            var imageModel = new ImageModel
            {
                Data = imageAsBase64
            };

            await requestService.PutAsync(uri, imageModel,AppSettings.AccessToken);
            //await CacheHelper.RemoveFromCache(profile.PhotoUrl);  
        }
    }
}
