using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.Authentication;
using RentIT.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class UtentePageViewModel : BaseViewModel
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
        readonly IUserService _userService;
        public UtentePageViewModel(INavService navService, AuthenticationService authService, UserService userService) : base(navService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async override Task Init()
        {
            //Questa pagina non è più raggiungibile senza che l'utente sia loggato quindi non c'è bisogno di quel controllo
            Utente = await _userService.GetCurrentProfileAsync();
            //Utente.Img = await _userService.GetUserImage();
        }

        //Comando per modificare i dati
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

        //Comando per visualizzare tutti gli annunci dell'utente
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

        //Comando per eseguire il log out
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
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Impossibile effettuare il log out", "Ok");
            }

            IsBusy = false;

        }
    }
}
