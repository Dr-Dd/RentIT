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

            await Task.Factory.StartNew(() =>
            {
                Annunci.Add(new Annuncio()
                {
                    NomeOggetto = "Tosaerba",
                    Descrizione = "Tosaerba BOSCHIA potente alimentato a merda di piccione",
                    Prezzo = 13,
                    PercorsoImmagine = "tosaerba.jpg",
                    NomeAffittuario = "Gigi",
                    CognomeAffittuario = "Finizio",
                    Posizione = "4Km da te",
                    Data = DateTime.Now
                });

                Annunci.Add(new Annuncio()
                {
                    NomeOggetto = "Tosaerba",
                    Descrizione = "Tosaerba BOSCHIA potente alimentato a merda di piccione",
                    Prezzo = 13,
                    PercorsoImmagine = "tosaerba.jpg",
                    NomeAffittuario = "Gigi",
                    CognomeAffittuario = "Finizio",
                    Posizione = "4Km da te",
                    Data = DateTime.Now
                });
            });
        }
    }
}
