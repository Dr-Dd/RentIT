using RentIT.Models;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        ObservableCollection<OggettoImmagine> _oggettiImmagine;
        public ObservableCollection<OggettoImmagine> OggettiImmagine
        {
            get
            {
                return _oggettiImmagine;
            }
            set
            {
                _oggettiImmagine = value;
                OnPropertyChanged();
            }
        }

        public GestioneAnnuncioViewModel(INavService navService) : base(navService)
        {
            
        }

        public async override Task Init(Annuncio annuncio)
        {
            Annuncio = annuncio;
            OggettiImmagine = creaOggettiImmagine(Annuncio);
        }

        public ObservableCollection<OggettoImmagine> creaOggettiImmagine(Annuncio annuncio)
        {
            OggettiImmagine = new ObservableCollection<OggettoImmagine>();
            foreach (FileImageSource image in Annuncio.PercorsiImmagine){
                OggettiImmagine.Add(new OggettoImmagine()
                {
                    Immagine = image
                });
            } 
            return OggettiImmagine;
        }
    }
}
