using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Models.User;

namespace App.Services.Annuncio
{
    public class AnnuncioService : IAnnuncioService
    {
        public Task<Response> AddAnnuncioAsync(RentIT.Models.Annuncio a)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<Response> ModifyItemData(RentIT.Models.Annuncio a)
        {
            throw new NotImplementedException();
        }

        public Task UploadItemImageAsync(string imageAsBase64)
        {
            throw new NotImplementedException();
        }
    }
}
