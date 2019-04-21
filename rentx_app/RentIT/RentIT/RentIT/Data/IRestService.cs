using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Data
{
    public interface IRestService
    {
        Task SaveUserAsync(Utente user);
        Task DeleteUserAsync(int ID);
        Task<string> AreCredentialsValid(Credentials cr);
    }
}
