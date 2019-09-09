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
    public class AnnunciPageViewModel : BaseViewModel<SearchQuery>
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

        public AnnunciPageViewModel(INavService navService) : base(navService)
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
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });


            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });


            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                NomeAffittuario = "Gigi Finizio",
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
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
            await NavService.NavigateTo<DettaglioAnnuncioViewModel, Annuncio>(annuncio);
        }

        public async override Task Init(SearchQuery query)
        {
            // TODO: Implementare la ricerca
            await LoadEntries();
        }
    }
}

