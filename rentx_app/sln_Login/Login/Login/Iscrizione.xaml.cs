using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Iscrizione : ContentPage
	{
        public Iscritti iscritti;
        public Iscrizione ()
        {
            InitializeComponent();
        }
		public Iscrizione (Iscritti iscr):this()
		{
			this.iscritti = iscr;
		}

        async private void Iscrivimi(object sender, EventArgs e)
        {
            if (usr.Text != null && psw.Text != null && Nome.Text != null && Cognome.Text != null)
            {
                User utente = new User(usr.Text, psw.Text, Nome.Text, Cognome.Text);
                if (!iscritti.aggiungiUtente(usr.Text, psw.Text,utente))
                {
                    fd.IsVisible = false;
                    usd.IsVisible = true;
                    usr.Text = null;
                }
                else
                {
                    fd.IsVisible = false;
                    usd.IsVisible = false;
                    usr.Text = null;
                    psw.Text = null;
                    Nome.Text = null;
                    Cognome.Text = null;
                    await Navigation.PushModalAsync(new Page2());
                }
            }
            else
                fd.IsVisible = true;
            
        }
    }
}