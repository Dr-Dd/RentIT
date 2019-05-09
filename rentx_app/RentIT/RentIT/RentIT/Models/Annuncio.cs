using System;
using System.Collections.Generic;
using System.Text;


namespace RentIt.Models
{
    class Annuncio
    {
        public string NomeOggetto { get; set; }

        public string Descrizione { get; set; }

        public decimal Prezzo { get; set; }

        public string NomeAffittuario { get; set; }

        public string CognomeAffittuario { get; set; }

        public string Posizione { get; set; }

        public DateTime Data { get; set; }

    }
}
