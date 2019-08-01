using RentIT.Models;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.ViewModels
{
    public class GestioneAnnuncioViewModel : BaseViewModel<Annuncio>
    {
        Annuncio _annuncio;
        public Annuncio Annuncio
        {
            get
            {
                return _annuncio;
            }
            set
            {
                _annuncio = value;
                OnPropertyChanged();
            }
        }

        public GestioneAnnuncioViewModel(INavService navService) : base(navService)
        {
        }

        public async override Task Init(Annuncio annuncio)
        {
            Annuncio = annuncio;
        }
    }
}
