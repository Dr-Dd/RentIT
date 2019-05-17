using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class TilePageViewModel : BaseViewModel<SearchQuery>
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

        public TilePageViewModel(INavService navService) : base(navService)
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

            // Crasha qui, per una funzione di test, ormai ho perso la speranza 
            // Carlo papaccio avevi dimenticato la viewCell !!

            //Chiedo scusa signore Lidone!!
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

        Command<Annuncio> _viewAnnuncio;
        public Command<Annuncio> ViewAnnuncio
        {
            get
            {
                return _viewAnnuncio
                    ?? (new Command<Annuncio>(async (annuncio) => await ExecuteViewAnnuncio(annuncio)));
            }
        }

        async Task ExecuteViewAnnuncio(Annuncio annuncio)
        {
            // TODO: Implementare una view "AnnuncioDetail"
            // await NavService.NavigateTo<AnnuncioDetailViewModel, Annuncio>(annuncio);
            // Ricordarsi che il suo ViewModel deve implementare BaseViewModel<Annuncio>
        }

        public async override Task Init(SearchQuery query)
        {
            // TODO: Implementare la ricerca
            await LoadEntries();
        }
    }
}

