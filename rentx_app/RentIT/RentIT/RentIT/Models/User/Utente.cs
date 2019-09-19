using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.Models.User
{
    public class Utente
    {
        // tutti i campi effettivi di una classe Utente , da decidere bene quali sono
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string numeroTel { get; set; }
        public string citta { get; set; }
    }
}