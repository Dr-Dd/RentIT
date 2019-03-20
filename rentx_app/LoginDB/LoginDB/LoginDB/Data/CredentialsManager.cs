using LoginDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace LoginDB.Data
{
    public class CredentialsManager
    {
        IRestService restService;

        public CredentialsManager(IRestService service)
        {
            restService = service;
        }

        public Task SaveTaskAsync(Utente user)
        {
            return restService.SaveUserAsync(user);
        }

        public Task DeleteTaskAsync(int id)
        {
            return restService.DeleteUserAsync(id);
        }

        public Task<string> AreCredentialsValid(Credentials cr)
        {
            return restService.AreCredentialsValid(cr);
        }
    }
}
