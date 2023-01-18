namespace back_end.DTOs.AccountDTOs
{
    public class AccountLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AccountLoginDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
