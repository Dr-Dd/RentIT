using System.Threading.Tasks;
using RentIT.Models.User;
using RentIT.Models.Annuncio;


namespace App.Services.Annuncio
{
    public interface IAnnuncioService
    {
        Task UploadItemImageAsync(string imageAsBase64);

        Task<Response> AddAnnuncioAsync(Ad a);

        Task<Response> ModifyItemData(Ad a);
        
        Task<Response> Delete(string id);
    }
}
