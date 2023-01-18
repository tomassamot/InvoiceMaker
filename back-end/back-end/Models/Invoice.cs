namespace back_end.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public List<Product> Products { get; set; }
        public int AccountId { get; set; }
        public Invoice()
        {
            Products = new List<Product>();
        }
        public Invoice(int id, DateTime dateTime, List<Product> products, int accountId)
        {
            Id = id;
            DateTime = dateTime;
            Products = products;
            AccountId = accountId;
        }
    }
}
