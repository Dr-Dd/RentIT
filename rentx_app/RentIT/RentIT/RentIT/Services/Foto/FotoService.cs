using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace App.Services.Foto
{
    public class FotoService
    {
        //Metodo per convertire dalla stringa base64 a un'immagine
        public Image fromStringToImage(string imageBase64)
        {
            //Conversione in un array di byte
            Byte[] bytes = Convert.FromBase64String(imageBase64);
            //Creazione immagine
            var img = new Image { Source = ImageSource.FromStream(() => new MemoryStream(bytes)) };
            return img;
        }

        //Metodo per convertire da uno stream a una stringa base64
        public string fromStreamToString(Stream stream)
        {
            MemoryStream copy = new MemoryStream();
            stream.CopyTo(copy);
            //Conversione in un array di byte
            byte[] bytes = copy.ToArray();
            //Creazione stringa
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
