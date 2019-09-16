using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace RentIT.Models
{
    /**Momentanea**/
    public class Annuncio
    {
        public string NomeOggetto { get; set; }

        public string Descrizione { get; set; }

        public ObservableCollection<FileImageSource> PercorsiImmagine { get; set; }

        public decimal Prezzo { get; set; }

        public string NomeAffittuario { get; set; }

        public string NomeRichiedente { get; set; }

        public string Posizione { get; set; }

        public DateTime Data { get; set; }

    }
}
