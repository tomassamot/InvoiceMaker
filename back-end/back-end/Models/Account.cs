using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Location { get; set; }
        public bool IsProvider { get; set; }
        public bool IsPayingVAT { get; set; }
        
        public Account()
        {
            Username = "";
            PasswordHash = "";
            PasswordSalt = "";
            Location = "";
        }
        public Account(int id, string username, string passwordHash, string passwordSalt, string location, bool isProvider, bool isPayingVAT)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Location = location;
            IsProvider = isProvider;
            IsPayingVAT = isPayingVAT;
        }
    }
}
