using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services;

namespace RentIT.ViewModels
{
    public class TilePageViewModel : BaseViewModel
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

        public override async Task Init()
        {
            await LoadEntries();
        }

        async Task LoadEntries()
        {
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
        }
    }
}

