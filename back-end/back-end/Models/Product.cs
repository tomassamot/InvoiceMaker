namespace back_end.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceWithVAT { get; set; }
        public int AccountId { get; set; }

        public Product()
        {
            Name = "";
            Description = "";
        }
        public Product(int id, string name, string description, decimal price, int accountId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            AccountId = accountId;
        }
    }
}
