using RentIT.Models.Annuncio;
using RentIT.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnnunciQueryPage : ContentPage
	{
        AnnunciQueryViewModel _vm
        {
            get { return BindingContext as AnnunciQueryViewModel; }
        }
		public AnnunciQueryPage ()
		{
			InitializeComponent ();
		}

        async void Annuncio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var annuncio = (Ad)e.Item;
            _vm.ViewAnnuncio.Execute(annuncio);

            listaAnnunci.SelectedItem = null;
        }

        public Image StringToImageConverter(string imageBase64)
        {
            Byte[] bytes = Convert.FromBase64String(imageBase64);
            //Creazione immagine
            var img = new Image { Source = ImageSource.FromStream(() => new MemoryStream(bytes)) };
            return img;
        }
    }
}