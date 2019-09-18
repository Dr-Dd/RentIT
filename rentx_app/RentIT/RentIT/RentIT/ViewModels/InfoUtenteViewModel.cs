using RentIT.Models;
using RentIT.Models.User;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class InfoUtenteViewModel : BaseViewModel<string>
    {
        /*Servirà in  un secondo momento quando si metterà Utente al posto di string
         * Utente _utente;
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
        */

        string _nomeUtente;
        public String NomeUtente
        {
            get { return _nomeUtente; }
            set
            {
                _nomeUtente = value;
                OnPropertyChanged();
            }
        }
        public InfoUtenteViewModel(INavService navService) : base(navService)
        {

        }

        public async override Task Init(string nomeUtente)
        {
            NomeUtente = nomeUtente;
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
            /*Qua in qualche modo andrà specificato che gli annunci sono relativi solo a quell'utente*/
            await NavService.NavigateTo<AnnunciPageViewModel>();
        }
    }
}
