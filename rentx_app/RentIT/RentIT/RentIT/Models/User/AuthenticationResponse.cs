namespace RentIT.Models.User
{
    public class AuthenticationResponse

    {
        public bool hasSucceded { get; set; }

        public int userId { get; set; }

        public string accessToken { get; set; }

        public string responseMessage { get; set; }
    }
}
