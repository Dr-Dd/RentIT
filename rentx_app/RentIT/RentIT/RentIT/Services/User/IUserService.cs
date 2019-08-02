
using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace RentIT.Services.User
{
    public interface IUserService
    {
        Task<SignUpResponse> SignUpAsync(SignUpRequest request);

        Task<Utente> GetCurrentProfileAsync();

        Task UploadUserImageAsync(string imageAsBase64);

    }
}
