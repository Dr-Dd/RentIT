using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LoginDB.Views
{
    // TODO: Notare come gli aspetti quali colore, e qualunque cosa sia di tipologia
    // visuale andrebbero spostati in un dizionario della view, riferirsi a Carlo
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private string title = "Rent[IT]";
        public string Title { get { return title; } }

        private string mailPlaceholder = "Mail";
        public string MailPlaceholder { get { return mailPlaceholder; } }

        private string pwdPlaceholder = "Password";
        public string PwdPlaceholder { get { return pwdPlaceholder; } }

        private string textColor = "White";
        public string TextColor { get { return textColor; } }

        private string errorMessageColor = "Red";
        public string ErrorMessageColor { get { return errorMessageColor; } }

        private bool isPwdTextPassword = true;
        public bool IsPwdTextPassword
        {
            get { return isPwdTextPassword; }
            // TODO: Feature che mostra la password momentaneamente (isPassword = false)
            // per questo c'è il setter
            set { isPwdTextPassword = value; }
        }

        // TODO: Questo è un messaggio che viene usato solo in caso di errore, con 
        // testo presentato in base alla situazione, progettarlo meglio
        private string userMessage;
        public string UserMessage
        {
            get { return userMessage; }
            set
            {
                userMessage = value;
            }
        }

        // TODO: Questo valore deve diventare true nel caso si verifichino degli
        // errori, progettare l'interfaccia di segnalazione errori
        private bool isUsrMsgVisible = false;
        public bool IsUsrMsgVisible
        {
            get { return IsUsrMsgVisible; }
            set
            {
                isUsrMsgVisible = value;
            }
        }

        private string loginButtonText = "LOG IN";
        public string LoginButtonText { get { return loginButtonText; } }

        private string loginButtonBackgroundColor = "#CDDC39";
        public string LoginButtonBackgroundColor { get { return loginButtonText; } }

        public LoginPageViewModel()
        {
        }

        //TODO: Implement propertyChange methods
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}