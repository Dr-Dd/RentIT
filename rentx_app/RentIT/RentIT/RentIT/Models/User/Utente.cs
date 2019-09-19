using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.Models.User
{
    public class Utente
    {
        // tutti i campi effettivi di una classe Utente , da decidere bene quali sono
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Numero { get; set; }
        public string Address { get; set; }
    }
}