using App.Models.Image;
using System;
using System.Collections.Generic;

namespace RentIT.Models.Annuncio
{

    public class Ad
    {

        public long Id { get; set; }

        public long AffittuarioId { get; set; }

        public string NomeOggetto { get; set; }

        public string Descrizione { get; set; }

        public string AnteprimaImg { get; set; } 
       
        public decimal Prezzo { get; set; }

        public string Posizione { get; set; }

        public DateTime Data { get; set; }

        //Questa è momentanea, le immagini vengono in realtà gestite solo nel vm
        public List<ImageModel> Immagini { get; set; }
    }
}
