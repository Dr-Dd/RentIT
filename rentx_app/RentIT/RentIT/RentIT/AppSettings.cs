using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT
{
    public class AppSettings
    {
        //utente attualmente loggato
        public static Utente User { get; set; }

        public static void RemoveUserData() => User = null;
    }
}
