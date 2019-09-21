using Newtonsoft.Json;
using RentIT.Models.Immagine;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RentIT.Models.Annuncio
{
    [JsonObject(IsReference = true)]
    public class Ad
    {

        public long id { get; set; }

        public long AffittuarioId { get; set; }

        public string anteprimaImg { get; set; }

        public Image anteprimaImgXam { get; set; }

        public string nomeOggetto { get; set; }

        public string descrizione { get; set; }
        
        public decimal prezzo { get; set; }

        public string posizione { get; set; }

        public DateTime data { get; set; }
    }
}
