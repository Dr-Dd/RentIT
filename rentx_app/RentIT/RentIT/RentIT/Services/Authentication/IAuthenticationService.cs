using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated { get; }

        Utente AuthenticatedUser { get; }

        Task<Models.Utente> IscriviUtenteAsync(Utente user);
        
        Task<Models.Utente> LoginAsync(string email, string password);

        Task<bool> LogoutAsync();
    }
}
