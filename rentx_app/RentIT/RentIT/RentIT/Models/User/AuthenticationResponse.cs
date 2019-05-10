namespace RentIT.Models.User
{
    public class AuthenticationResponse

    {
        public bool HasSucceded { get; set; }

        public int UserId { get; set; }

        public string AccessToken { get; set; }

        public string ResponseMessage { get; set; }
    }
}
