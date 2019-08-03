using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    /**
     * Classe segnaposto, ancora da definire meglio
     * 
     * IMPORTANTE:
     * Qui probabilmente sarebbe più intelligente utilizzare il
     * token per ricavare le informazioni dal database persistente,
     * se utilizzassimo l'approccio di passare l'utente da view a view,
     * probabilmente finiremo per riempire quasi tutti i viewModel con un
     * Utente (e avremo bisogno di un metodo generico con 3 parametri,
     * contro i due utilizzati ora)
     */
    public class UtentePageViewModel : BaseViewModel<Utente>
    {
        Utente _utente;
        public Utente Utente
        {
            get { return _utente; }
            set
            {
                _utente = value;
                OnPropertyChanged();
            }
        }

        readonly IAuthenticationService _authService;
        public UtentePageViewModel(INavService navService, AuthenticationService authService) : base(navService)
        {
            _authService = authService;
        }

        public async override Task Init(Utente utente)
        {
            await LoadMockData();
        }

        // Funzione con dati sample
        public async Task LoadMockData()
        {
            Utente = new Utente()
            {
                Name = "Gigi Finizio",
                Img = "meme.png"
            };
        }

        Command _modificaCommand;
        public Command ModificaCommand
        {
            get
            {
                return _modificaCommand
                    ?? (_modificaCommand = new Command(async () => await ExecuteModificaCommandAsync()));
            }
        }

        async Task ExecuteModificaCommandAsync()
        {
            await NavService.NavigateTo<ModificaDatiViewModel>();
        }

        Command _annunciUtenteCommand;
        public Command AnnunciUtenteCommand
        {
            get
            {
                return _annunciUtenteCommand
                    ?? (_annunciUtenteCommand = new Command(async () => await ExecuteAnnunciUtenteCommandAsync()));
            }
        }

        async Task ExecuteAnnunciUtenteCommandAsync()
        {
            await NavService.NavigateTo<AnnunciUtenteViewModel>();
        }

        Command _logOutCommand;
        public Command LogOutCommand
        {
            get
            {
                return _logOutCommand
                    ?? (_logOutCommand = new Command(async () => await ExecuteLogOutCommandAsync()));
            }
        }

        async Task ExecuteLogOutCommandAsync()
        {
            IsBusy = true;

            var response = await _authService.LogoutAsync();
            if (response)
            {
                await NavService.ClearBackStack();
                await NavService.NavigateTo<SearchPageViewModel>();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Impossibile effettuare il log out", "Ok");
            }

            IsBusy = false;

        }
    }
}
