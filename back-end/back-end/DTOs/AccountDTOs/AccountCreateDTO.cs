using Microsoft.AspNetCore.Authentication;

namespace back_end.DTOs.AccountDTOs
{
    /*public class AccountCreateDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Location { get; set; }
        public bool IsProvider { get; set; }
        public bool IsPayingVAT { get; set; }

        public AccountCreateDTO(string username, string passwordHash, string passwordSalt, string location, bool isProvider, bool isPayingVAT)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Location = location;
            IsProvider = isProvider;
            IsPayingVAT = isPayingVAT;
        }
    }*/
    public class AccountCreateDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Location { get; set; }
        public bool IsProvider { get; set; }
        public bool IsPayingVAT { get; set; }

        public AccountCreateDTO(string username, string passwordHash, string passwordSalt, string location, bool isProvider, bool isPayingVAT)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Location = location;
            IsProvider = isProvider;
            IsPayingVAT = isPayingVAT;
        }
    }
}
