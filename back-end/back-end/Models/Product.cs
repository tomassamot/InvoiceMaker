namespace back_end.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SumWithVAT { get; set;}
        public decimal SumWithoutVAT { get; set;}
        public int AccountId { get; set; }

        public Product()
        {
            Name = "";
            Description = "";
        }
        public Product(int id, string name, string description, decimal sumWithVAT, decimal sumWithoutVAT, int accountId)
        {
            Id = id;
            Name = name;
            Description = description;
            SumWithVAT = sumWithVAT;
            SumWithoutVAT = sumWithoutVAT;
            AccountId = accountId;
        }
    }
}
