using App.Models.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.Services.Foto
{
    public class FotoService
    {
        /*Metodo per convertire dalla stringa base64,
         * ovvero da un'immagine del tipo restituito dal db a un'image*/
        public Image fromStringToImage(string imageBase64)
        {
            //Conversione in un array di byte
            Byte[] bytes = Convert.FromBase64String(imageBase64);
            //Creazione immagine
            var img = new Image { Source = ImageSource.FromStream(() => new MemoryStream(bytes)) };
            return img;
        }

        /*Metodo per convertire da uno stream a una stringa base64
         * Serve ad esempio a salvare nel db un'immagine dalla galleria del telefono
         * dato che con PicturePicker viene caricata come stream*/
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

        /* Metodo per prendere dal db la foto di un utente conoscendone l'email*/
        public Task<ImageModel> GetImage(string email)
        {
            return new Task<ImageModel>(() => new ImageModel());            
        }
    }
}
