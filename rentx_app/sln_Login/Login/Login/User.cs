using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Login
{
	public class User : ContentPage
	{
        public int id;
        public String nome { get; set; }
        public String cognome { get; set; }
        public String username;
        public String password;
        public String email;

        public User (){}
        public User(String user,String pass,String nome,String cgn)
        {
            this.username = user;
            this.password = pass;
            this.nome = nome;
            this.cognome = cgn;
        }

        
	}
}