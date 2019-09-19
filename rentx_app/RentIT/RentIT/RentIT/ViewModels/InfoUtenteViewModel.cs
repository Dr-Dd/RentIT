using RentIT.Models.Annuncio;
using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.Annuncio;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class InfoUtenteViewModel : BaseViewModel<Utente>
    {
        Utente _utente;
        public Utente Utente
        {
            get
            {
                return _utente;
            }
            set
            {
                _utente = value;
                OnPropertyChanged();
            }
        }

        readonly IAnnuncioService _annuncioService;
        public InfoUtenteViewModel(INavService navService, AnnuncioService annuncioService) : base(navService)
        {
            _annuncioService = annuncioService;
        }

        public async override Task Init(Utente utente)
        {
            Utente = utente;
        }

        Command _vediAnnunciUtenteCommand;
        public Command VediAnnunciUtenteCommand
        {
            get
            {
                return _vediAnnunciUtenteCommand
                    ?? (_vediAnnunciUtenteCommand = new Command(async () => await ExecuteVediAnnunciUtenteCommand()));
            }
        }

        async Task ExecuteVediAnnunciUtenteCommand()
        {
            ObservableCollection<Ad> asd = await _annuncioService.GetUserAds(Utente.id, false);
            await NavService.NavigateTo<AnnunciPageViewModel, ObservableCollection<Ad>>(asd);
        }
    }
}
