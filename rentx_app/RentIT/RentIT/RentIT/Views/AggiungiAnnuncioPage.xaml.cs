using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentIT.Services;
using RentIT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AggiungiAnnuncioPage : ContentPage
	{

        AggiungiAnnuncioViewModel _vm
        {
            get { return _vm as AggiungiAnnuncioViewModel; }
        }

        public AggiungiAnnuncioPage()
        {
            InitializeComponent();
        }

        /**Metodo che consente la selezione di un' immagine dalla galleria**/
        private async void Pick_Image_Button_Clicked(object sender, EventArgs e)
        {
            pickPictureButton.IsEnabled = false;
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                image.Source = ImageSource.FromStream(() => stream);

                TapGestureRecognizer recognizer = new TapGestureRecognizer();
                recognizer.Tapped += (sender2, args) =>
                {
                    pickPictureButton.IsEnabled = true;
                };
                image.GestureRecognizers.Add(recognizer);
            }
            else
            {
                pickPictureButton.IsEnabled = true;
            }
            
        }
    }
}