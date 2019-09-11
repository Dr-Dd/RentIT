using System.Threading.Tasks;
using RentIT.Models.User;

namespace App.Services.Annuncio
{
    public interface IAnnuncioService
    {
        Task UploadItemImageAsync(string imageAsBase64);

        Task<Response> AddAnnuncioAsync(RentIT.Models.Annuncio a);

        Task<Response> ModifyItemData(RentIT.Models.Annuncio a);
        //Non so perché ma annuncio non me lo prende se metto using

        Task<Response> Delete();
    }
}
