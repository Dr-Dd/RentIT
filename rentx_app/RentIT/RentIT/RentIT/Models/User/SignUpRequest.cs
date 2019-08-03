using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.Models.User
{
    public class SignUpRequest
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
