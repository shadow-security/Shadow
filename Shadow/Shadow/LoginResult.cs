namespace Shadow
{
    public class LoginResult
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string SocialId
        {
            get { return string.IsNullOrEmpty(Sub) ? Id : Sub; }
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Sub { get; set; }
        public string Id { get; set; }
    }
}