using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Models.User;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


//Da modificare
namespace RentIT.ViewModels
{
    public class AnnunciUtenteViewModel : BaseViewModel<SearchQuery>
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
        
        public AnnunciUtenteViewModel(INavService navService) : base(navService)
        {
            Annunci = new ObservableCollection<Ad>();
        }
        /**
        * IMPORTANTE: Nello stato attuale, la ListView fa laggare
        * vistosamente l'app, trovare un modo di rendere più veloce
        * ed efficiente lo scroll
        
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
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escrementi di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escrementi di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            Annunci.Add(new Ad()
            {
                NomeOggetto = "Tosaerba",
                Descrizione = "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione",
                Prezzo = 13,
                Immagine = new Image { Source = "tosaerba.jpg" },
                Affittuario = new Utente { Name = "Gigi Finizio" },
                Posizione = "4 Km da te",
                Data = DateTime.Now
            });

            IsBusy = false;
        }*/

        Command<Ad> _viewGestioneAnnuncio;
        public Command<Ad> ViewGestioneAnnuncio
        {
            get
            {
                return _viewGestioneAnnuncio
                    ?? (new Command<Ad>(async (annuncio) => await ExecuteViewGestioneAnnuncio(annuncio)));
            }
        }

        async Task ExecuteViewGestioneAnnuncio(Ad annuncio)
        {
            await NavService.NavigateTo<GestioneAnnuncioViewModel, Ad>(annuncio);
        }

        public async override Task Init(SearchQuery query)
        {
            //await LoadEntries(); 
        }
    }
}
