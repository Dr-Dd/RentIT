using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.DBmessages
{
    public class Response
    {
        public bool hasSucceded { get; set; }
        public string responseMessage { get; set; }
        public long idGen { get; set; }     //generico id in questo caso per gli annunci
    }
}
