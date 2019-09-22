using RentIT.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using RentIT.Services.User;
using RentIT.Models.User;
using RentIT.Services.Foto;
using RentIT.Models.Immagine;
using System.Text;
using System.Collections.Generic;

namespace RentIT.ViewModels
{
    public class ModificaDatiViewModel : BaseViewModel
	{

        List<string> _listaCitta;
        public List<string> ListaCitta
        {
            get { return _listaCitta; }
            set
            {
                _listaCitta = value;
                OnPropertyChanged();
            }
        }

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
            get { return _immagine; }
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
            Immagine = new Image();
            ListaCitta = MiscCostants.tutteCitta;
        }

        public override async Task Init()
        {
            IsBusy = true;
            Utente = await _userService.GetMyProfileAsync();
            Immagine = await getPropic();
            IsBusy = false;
        }

        //Metodo per prendere l'immagine profilo dal database
        public async Task<Image> getPropic()
        {
            ImageModel imgModel = await _fotoService.GetUserImage(Utente.id);
            if (imgModel.data != null)
            {
                Image img = _fotoService.fromStringToImage(imgModel.data);
                return img;
            }
            else
            {
                Image img = new Image();
                img.Source = "meme.png";
                return img;
            }
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
            var isValid = Password.Trim() == ConfermaPassword.Trim();

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

            Utente.name = Utente.name.Trim();
            Utente.surname = Utente.surname.Trim();
            Utente.citta = Utente.citta.Trim();
            Utente.numeroTel = Utente.numeroTel.Trim();
            
            if (!string.IsNullOrWhiteSpace(Password))
            {
                if (!PasswordsAreTheSame())
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Le due password non corrispondono", "Ok");
                    IsBusy = false;
                    return;
                }
                Utente.password = Password.Trim();
            }
                
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
                IsBusy = false;
                StringBuilder successo = new StringBuilder();
                successo.AppendLine("Ci dispiace che tu te ne vada!");
                successo.Append("I tuoi dati sono stati eliminati correttamente");
                await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "ok");
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
