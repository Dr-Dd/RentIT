using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.Models
{
    public class Utente
    {
        public int  id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Boolean isBlankfield()
        {
            return this.name == null || this.surname == null || this.email == null || this.password == null;
        }
     }
}
