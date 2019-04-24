using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace RentIT.ViewModels
{
    // TODO: Notare come gli aspetti quali colore, e qualunque cosa sia di tipologia
    // visuale andrebbero spostati in un dizionario della view, riferirsi a Carlo
    public class LoginPageViewModel : BaseViewModel
    {
        string _email;
        public string Email
        {
            get { return _email;  }
            set
            {
                _email = value;
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
    }
}