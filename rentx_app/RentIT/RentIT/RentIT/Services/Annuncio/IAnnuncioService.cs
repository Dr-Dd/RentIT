using System.Threading.Tasks;
using RentIT.Models.User;
using RentIT.DBmessages;
using RentIT.Models.Annuncio;
using System.Collections.Generic;
using RentIT.Models;
using System.Collections.ObjectModel;

namespace App.Services.Annuncio
{
    public interface IAnnuncioService
    {
        Task<Response> AddAnnuncioAsync(Ad a);

        Task<Response> ModifyAdDataAsync(Ad a);
        
        Task<Response> DeleteAdAsync(int idAnn);

        Task<ObservableCollection<Ad>> GetLastAds(string citta,string oggetto);

        Task<ObservableCollection<Ad>> GetUserAds(long UserId,bool b);

        Task<ObservableCollection<Ad>> GetMyNotBookedAds();

        Task<ObservableCollection<Ad>> GetMyBookedAds();
    }
}
