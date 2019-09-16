using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RentIT.Models.Annuncio
{
    
    public class Ad
    {

        public int Id { get; set; }

        public string NomeOggetto { get; set; }

        public string Descrizione { get; set; }

        public string AnteprimaImg { get; set; }
       
        public decimal Prezzo { get; set; }

        public string Posizione { get; set; }

        public DateTime Data { get; set; }

        public int AffittuarioId { get; set; }

        //Image non ci va proprio
        // public Image Immagine { get; set; }

        // public Utente Affittuario { get; set; }
    }
}
