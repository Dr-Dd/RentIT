using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Login
{
    public class Iscritti : ContentPage
    {
        public IDictionary<String, String> mappa { get; set; }
        public IDictionary<String, User> mappa2 { get; set; }
        public Iscritti()
        {
            this.mappa = new Dictionary<String, String>();
            this.mappa2 = new Dictionary<String, User>();
        }
        public bool aggiungiUtente(String user, String pass, User utente)
        {
            mappa.TryGetValue(user, out String v);
            if (v != null)
                return false;
            else
            {
                mappa.Add(user, pass);
                mappa2.Add(user, utente);
                return true;
            }
        }
        public bool Check(String usr, String pass)
        {
            mappa.TryGetValue(usr, out String value);
            if (value != null)
            {
                if (pass.Equals(value))
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        public User dammiUtente(String s)
        {
            if (s != null)
            {
                this.mappa2.TryGetValue(s, out User value);
                return value;
            }
            else
                return null;
        }
    }
}