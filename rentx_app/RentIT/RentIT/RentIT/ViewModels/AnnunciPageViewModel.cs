using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RentIT.Services;
using Xamarin.Forms;
using RentIT.Models.Annuncio;

namespace RentIT.ViewModels
{
    public class AnnunciPageViewModel : BaseViewModel<ObservableCollection<Ad>>
    {

        ObservableCollection<Ad> _annunci;
        public ObservableCollection<Ad> Annunci
        {
            get { return _annunci; }
            set
            {
                _annunci = value;
                OnPropertyChanged();
            }
        }

        public AnnunciPageViewModel(INavService navService) : base(navService)
        {
        }


        public async override Task Init(ObservableCollection<Ad> annunci)
        {
            Annunci = annunci;
        }

        /**
         * IMPORTANTE: Nello stato attuale, la ListView fa laggare
         * vistosamente l'app, trovare un modo di rendere più veloce
         * ed efficiente lo scroll
         */

        Command<Ad> _viewAnnuncio;
        public Command<Ad> ViewAnnuncio
        {
            get
            {
                return _viewAnnuncio
                    ?? (new Command<Ad>(async (annuncio) => await ExecuteViewAnnuncio(annuncio)));
            }
        }

        async Task ExecuteViewAnnuncio(Ad annuncio)
        {
            await NavService.NavigateTo<DettaglioAnnuncioViewModel, Ad>(annuncio);
        }
    }
}

