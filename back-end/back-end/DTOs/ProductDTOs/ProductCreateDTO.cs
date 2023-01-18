using Microsoft.AspNetCore.Authentication;

namespace back_end.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AccountId { get; set; }

        public ProductCreateDTO(string name, string description, decimal price, int accountId)
        {
            Name = name;
            Description = description;
            Price = price;
            AccountId = accountId;
        }
    }
}
