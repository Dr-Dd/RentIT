using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.Models.User
{
    public class Utente
    {
        // tutti i campi effettivi di una classe Utente , da decidere bene quali sono 
        public string Name { get; set; }
        private string Surname { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
        private string NumeroTel { get; set; }
        private string Address { get; set; }
    }
}
