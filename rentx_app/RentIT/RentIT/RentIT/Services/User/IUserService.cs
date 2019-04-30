using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Services.User
{
    public interface IUserService
    {
        Task<Utente> CreateUserAsync(Models.Utente user);

    }
}
