namespace back_end.DTOs.AccountDTOs
{
    public class AccountLoginDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public AccountLoginDTO(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }
    }
}
