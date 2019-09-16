using System.Threading.Tasks;
using RentIT.Models.User;
using RentIT.Models.Annuncio;


namespace App.Services.Annuncio
{
    public interface IAnnuncioService
    {
        Task<Response> AddAnnuncioAsync(Ad a);

        Task<Response> ModifyAdDataAsync(Ad a);
        
        Task<Response> DeleteAdAsync(string idAnn);
    }
}
