namespace LMS.Models
{
    public class Account
    {
        public int Id { get; set; }
        public required string Username { get; set; }

        public required string Password { get; set; }

        public required AccountType AccountType { get; set; }
    }
}
