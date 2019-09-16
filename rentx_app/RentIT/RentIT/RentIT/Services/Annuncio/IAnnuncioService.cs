using System.Threading.Tasks;
using RentIT.Models.User;
using RentIT.DBmessages;
using RentIT.Models.Annuncio;
using System.Collections.Generic;
using RentIT.Models;

namespace App.Services.Annuncio
{
    public interface IAnnuncioService
    {
        Task<Response> AddAnnuncioAsync(Ad a);

        Task<Response> ModifyAdDataAsync(Ad a);
        
        Task<Response> DeleteAdAsync(int idAnn);

        Task<List<Ad>> GetLastAds(string citta,string oggetto);

        Task<List<Ad>> GetUserAds(long UserId,bool b);

    }
}
