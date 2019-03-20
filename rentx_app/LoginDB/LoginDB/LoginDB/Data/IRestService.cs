using LoginDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginDB.Data
{
    public interface IRestService
    {
        Task SaveUserAsync(Utente user);
        Task DeleteUserAsync(int ID);
        Task<String> AreCredentialsValid(Credentials cr);
    }
}
