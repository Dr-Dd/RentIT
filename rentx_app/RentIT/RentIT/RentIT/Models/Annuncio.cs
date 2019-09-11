using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RentIT.Models
{
    /**Momentanea**/
    public class Annuncio
    {
        public string NomeOggetto { get; set; }

        public string Descrizione { get; set; }

        public Image Immagine { get; set; }

        public decimal Prezzo { get; set; }

        public Utente Affittuario { get; set; }

        public string Posizione { get; set; }

        public DateTime Data { get; set; }

    }
}
