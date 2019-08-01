using RentIT.Models;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;


//Da modificare
namespace RentIT.ViewModels
{
    public class AnnunciUtenteViewModel : BaseViewModel<SearchQuery>
    {
        ObservableCollection<Annuncio> _annunci;
        public ObservableCollection<Annuncio> Annunci
        {
            get { return _annunci; }
            set
            {
                _annunci = value;
                OnPropertyChanged();
            }
        }
        
        public AnnunciUtenteViewModel(INavService navService) : base(navService)
        {
            Annunci = new ObservableCollection<Annuncio>();
        }
        /**
        * IMPORTANTE: Nello stato attuale, la ListView fa laggare
        * vistosamente l'app, trovare un modo di rendere più veloce
        * ed efficiente lo scroll
        */
        async Task LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            Annunci.Clear();

            // TODO: Aggiungere persistenza database
            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escrementi di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });


            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });


            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                PercorsoImmagine = "tosaerba.jpg",
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            IsBusy = false;
        }

        public async override Task Init(SearchQuery query)
        {
            await LoadEntries(); 
        }
    }
}
