using RentIT.Models;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

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
        
    }
}
