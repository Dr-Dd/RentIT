using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RentIt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TilePage : ContentPage
	{
        private List<Annuncio> _annunci = new List<Annuncio>();

		public TilePage ()
		{
			InitializeComponent ();

            //Oggetto prova
            _annunci.Add(new Annuncio()
            {
                NomeOggetto = "Tosaerba",
                NomeAffittuario = "Gigi",
                CognomeAffittuario = "Finizio",
                Data = DateTime.Now,
                Prezzo = 13,
                Descrizione = "Tosaerba di ultima generazione marca BOSCHIA ottime condizioni",
                Icon = "tosaerba.jpg"

            });
		}

    }
}