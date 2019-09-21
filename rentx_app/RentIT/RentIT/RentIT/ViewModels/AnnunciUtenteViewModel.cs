using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using RentIT.Services.Foto;

namespace RentIT.ViewModels
{
    public class AnnunciUtenteViewModel : BaseViewModel
    {
        public AnnunciUtenteViewModel(INavService navService) : base(navService)
        {
        }

        public async override Task Init()
        {
        }
    }
}
