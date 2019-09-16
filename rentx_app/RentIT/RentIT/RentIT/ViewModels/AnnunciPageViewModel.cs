using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services;
using Xamarin.Forms;
using RentIT.Models.User;
using RentIT.Models.Annuncio;

namespace RentIT.ViewModels
{
    public class AnnunciPageViewModel : BaseViewModel<SearchQuery>
    {

        ObservableCollection<Ad> _annunci;
        public ObservableCollection<Ad> Annunci
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
            Annunci = new ObservableCollection<Ad>();
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
            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escrementi di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });


            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });


            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            IsBusy = false;
        }

        Command<Ad> _viewAnnuncio;
        public Command<Ad> ViewAnnuncio
        {
            get
            {
                return _viewAnnuncio
                    ?? (new Command<Ad>(async (annuncio) => await ExecuteViewAnnuncio(annuncio)));
            }
        }

        async Task ExecuteViewAnnuncio(Ad annuncio)
        {
            await NavService.NavigateTo<DettaglioAnnuncioViewModel, Ad>(annuncio);
        }

        public async override Task Init(SearchQuery query)
        {
            // TODO: Implementare la ricerca
            await LoadEntries();
        }
    }
}

