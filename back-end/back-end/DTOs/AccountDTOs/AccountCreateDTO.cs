using back_end.Models;
using Microsoft.AspNetCore.Authentication;

namespace back_end.DTOs.AccountDTOs
{
    public class AccountCreateDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LocationName { get; set; }
        public string LocationRegion { get; set; }
        public float LocationVAT { get; set; }
        public bool IsProvider { get; set; }
        public bool IsPayingVAT { get; set; }

        public AccountCreateDTO(string username, string password, string confirmPassword, string locationName, string locationRegion, float locationVAT, bool isProvider, bool isPayingVAT)
        {
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;
            LocationName = locationName;
            LocationRegion = locationRegion;
            LocationVAT = locationVAT;
            IsProvider = isProvider;
            IsPayingVAT = isPayingVAT;
        }
    }
}
