using Microsoft.AspNetCore.Authentication;

namespace back_end.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SumWithVAT { get; set; }
        public decimal SumWithoutVAT { get; set; }
        public int AccountId { get; set; }

        public ProductCreateDTO(string name, string description, decimal sumWithVAT, decimal sumWithoutVAT, int accountId)
        {
            Name = name;
            Description = description;
            SumWithVAT = sumWithVAT;
            SumWithoutVAT = sumWithoutVAT;
            AccountId = accountId;
        }
    }
}
