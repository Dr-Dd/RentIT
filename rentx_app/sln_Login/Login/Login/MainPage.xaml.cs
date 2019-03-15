using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Login
{
    public partial class MainPage : ContentPage
    {
        public Iscritti iscritti;
        public MainPage()
        {
            InitializeComponent();
            this.iscritti = new Iscritti();
        }

        async private void SignInProcedure(object sender, System.EventArgs e)
        {

            if (usr.Text != null && psw.Text != null)
            {
                if (iscritti.Check(usr.Text, psw.Text))
                {
                    User u = this.iscritti.dammiUtente(usr.Text);
                    usr.Text = null;
                    psw.Text = null;
                    Valide.IsVisible = false;
                    await Navigation.PushModalAsync(new Page1(u.nome, u.cognome));
                }
                else
                    Valide.IsVisible = true;
            }
            else
                Valide.IsVisible = true;
            }
        

        async private void Iscriviti(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Iscrizione(iscritti));
        }
    }
}
