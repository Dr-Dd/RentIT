using RentIT.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using RentIT.Services.User;
using RentIT.Models.User;
using RentIT.Services.Foto;
using RentIT.Models.Immagine;

namespace RentIT.ViewModels
{
    public class ModificaDatiViewModel : BaseViewModel
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

        Image _immagine;
        public Image Immagine
        {
            //se _immagine è null, non ci sono foto nel db e ne carica una di default
            get { return _immagine ?? (new Image { Source = "meme.png" }); }
            set
            {
                _immagine = value;
                OnPropertyChanged();
            }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        string _confermaPassword;
        public string ConfermaPassword
        {
            get { return _confermaPassword; }
            set
            {
                _confermaPassword = value;
                OnPropertyChanged();
            }
        }                                                         

        readonly IUserService _userService;
        readonly FotoService _fotoService;
        public ModificaDatiViewModel (INavService navService, UserService userService, FotoService fotoService) : base(navService)
		{
            _userService = userService;
            _fotoService = fotoService;
        }

        public override async Task Init()
        {
            Utente = await _userService.GetMyProfileAsync();
            //TODO: da decommentare dopo fatto il collegamento al db
            //Immagine = await _fotoService.getPropic();
        }

        //Metodo per prendere l'immagine profilo dal database
        public async Task<Image> getPropic()
        {
            ImageModel foto = await _fotoService.GetUserImage(AppSettings.UserId);
            Image img = null;
            if (foto != null)
                img = _fotoService.fromStringToImage(foto.data);
            return img;

        }

        //Comando per aggiungere o modificare la propic
        Command _modificaFotoCommand;
        public Command ModificaFotoCommand
        {
            get
            {
                return _modificaFotoCommand
                    ?? (_modificaFotoCommand = new Command(async () => await ExecuteModificaFotoCommand()));
            }
        }

        async Task ExecuteModificaFotoCommand()
        {
            //Caricare un'immagine dalla galleria
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                //se esiste, si salva la foto nel db associandola all'utente
                string base64 = _fotoService.fromStreamToString(stream);
                await _fotoService.UploadUserImageAsync(base64);
                //e poi si visualizza
                Immagine = _fotoService.fromStringToImage(base64);
                OnPropertyChanged();
            }
        }

        //Funzione di controllo password
        bool PasswordsAreTheSame()
        {
            var isValid = Password == ConfermaPassword;

            return isValid;
        }

        //Funzione per verificare che siano stati inseriti sia il numero che la città
        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(Utente.name) ||
                        string.IsNullOrWhiteSpace(Utente.surname) ||
                        string.IsNullOrWhiteSpace(Utente.citta) ||
                        string.IsNullOrWhiteSpace(Utente.numeroTel);
            return empty;
        }

        /*Comando per modificare i dati dell'account*/
        Command _modificaDatiCommand;
        public Command ModificaDatiCommand
        {
            get
            {
                return _modificaDatiCommand
                    ?? (_modificaDatiCommand = new Command(async () => await ExecuteModificaDatiCommand()));
            }
        }

        async Task ExecuteModificaDatiCommand()
        {
            IsBusy = true;
            
            if (EmptyFields())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Devi inserire sia il numero che la città", "Ok");
                return;
            }

            if (!PasswordsAreTheSame())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Le due password non corrispondono", "Ok");
                return;
            }

            if (!string.IsNullOrWhiteSpace(Password))
                Utente.password = Password;

            var response = await _userService.ModifyUserData(Utente);
            if (response.hasSucceded)
            {
                AppSettings.NewProfile = false;
                await App.Current.MainPage.DisplayAlert("RentIT", response.responseMessage, "Ok");
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }
            IsBusy = false;
        }

        /*Comando per eliminare l'account*/
        Command _eliminaCommand;
        public Command EliminaCommand
        {
            get
            {
                return _eliminaCommand
                    ?? (_eliminaCommand = new Command(async () => await ExecuteEliminaCommand()));
            }
        }

        async Task ExecuteEliminaCommand()
        {
            IsBusy = true;
            var response = await _userService.DeleteAccount();
            if(response.hasSucceded)
            {
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }
            IsBusy = false;
        }
    }
}