using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace back_end.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LocationName { get; set; }
        public string LocationRegion { get; set; }
        public float LocationVAT { get; set; }
        public bool IsProvider { get; set; }
        public bool IsPayingVAT { get; set; }
        
        public Account(int id, string username, string password, string locationName, string locationRegion, float locationVAT, bool isProvider, bool isPayingVAT)
        {
            Id = id;
            Username = username;
            Password = password;
            LocationName = locationName;
            LocationRegion = locationRegion;
            LocationVAT = locationVAT;
            IsProvider = isProvider;
            IsPayingVAT = isPayingVAT;
        }
        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString) // salt required
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
