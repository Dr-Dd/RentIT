using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models.User;
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

        public Task<SignUpResponse> SignUpAsync(Utente user)
        {
            // RestUrl = http://5.249.151.26:5000/
            var builder = new UriBuilder(Constants.UserEndpointIscrizione());
            var uri = builder.ToString();

            string token = "";

            return requestService.PostAsync<Utente,SignUpResponse>(uri, user, token);
        }
    }
}
