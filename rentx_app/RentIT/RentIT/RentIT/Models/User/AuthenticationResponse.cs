namespace RentIT.Models.User
{
    public class AuthenticationResponse

    {
        public bool hasSucceded { get; set; }

        public long id { get; set; }

        public string accessToken { get; set; }

        public string responseMessage { get; set; }

        public bool isFirstAccess { get; set; }
    }
}
