using RentIT.Models;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;


namespace RentIT.ViewModels
{
    /**
     * Classe segnaposto, probabilmente in futuro ci sarà bisogno 
     * di implementare l'aggiunta di un oggetto tramite API
     */
    public class DettaglioAnnuncioViewModel : BaseViewModel<Annuncio>
    {
        Annuncio _annuncio;
        public Annuncio Annuncio
        {
            get { return _annuncio; }
            set
            {
                _annuncio = value;
                OnPropertyChanged();
            }
        }

        public DettaglioAnnuncioViewModel(INavService navService) : base(navService)
        {
        }

        public async override Task Init(Annuncio annuncio)
        {
            Annuncio = annuncio;
        }

        Command _callCommand;
        public Command CallCommand
        {
            get
            {
                return _callCommand
                    ?? (_callCommand = new Command(async () => await ExecuteCallCommand()));
            }
        }

        async Task ExecuteCallCommand()
        {
            try
            {
                PhoneDialer.Open("6666666666666");
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.  
            }
            catch (Exception ex)
            {
                // Other error has occurred.  
            }
        }

        Command _emailCommand;
        public Command EmailCommand
        {
            get
            {
                return _emailCommand
                    ?? (_emailCommand = new Command(async () => await ExecuteEmailCommand()));
            }
        }

        async Task ExecuteEmailCommand()
        {
            List<String> destinatario = new List<string>();
            destinatario.Add("tiziocaio@papapa.it");
            var message = new EmailMessage
            {
                To = destinatario,
            };

            await Email.ComposeAsync(message);
        }
    }
}
