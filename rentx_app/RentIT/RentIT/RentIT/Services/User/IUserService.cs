
using RentIT.DBmessages;
using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace RentIT.Services.User
{
    public interface IUserService
    {
        Task<Response> SignUpAsync(SignUpRequest request);

        Task<Utente> GetMyProfileAsync();

        Task<Response> ModifyUserData(Utente u);

        Task<Response> DeleteAccount();

        Task<Utente> GetUserByIdAsync(long userId);
    }
}
