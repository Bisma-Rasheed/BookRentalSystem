namespace BookRentalSystem.Models.EmailConfig
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; } = null;
        public int Port { get; set; }
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;
    }
}
